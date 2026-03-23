(() => {
  const page = document.querySelector('[data-weather-url]');
  if (!page) {
    return;
  }

  const endpoint = page.dataset.weatherUrl;
  if (!endpoint) {
    return;
  }

  const statusText = document.querySelector('[data-status-text]');
  const loadingSection = document.querySelector('[data-loading]');
  const errorSection = document.querySelector('[data-error]');
  const errorMessage = document.querySelector('[data-error-message]');
  const retryButton = document.querySelector('[data-retry]');
  const cityEl = document.querySelector('[data-city-name]');
  const updatedEl = document.querySelector('[data-last-updated]');
  const tempEl = document.querySelector('[data-temperature]');
  const conditionEl = document.querySelector('[data-condition-text]');
  const feelsEl = document.querySelector('[data-feels-like]');
  const windEl = document.querySelector('[data-wind]');
  const humidityEl = document.querySelector('[data-humidity]');
  const pressureEl = document.querySelector('[data-pressure]');
  const hourlyStrip = document.querySelector('[data-hourly]');
  const dailyList = document.querySelector('[data-daily]');
  const currentIconEl = document.querySelector('[data-current-icon]');

  const roundTemp = (value) => {
    if (value == null || Number.isNaN(Number(value))) {
      return '—';
    }
    const rounded = Math.round(Number(value));
    return `${rounded > 0 ? '+' : ''}${rounded}°`;
  };

  const formatTime = (value) => {
    if (!value) {
      return '—';
    }
    return new Date(value).toLocaleTimeString('ru-RU', {
      hour: '2-digit',
      minute: '2-digit',
      timeZone: 'Europe/Moscow',
    });
  };

  const formatDay = (value) => {
    if (!value) {
      return '—';
    }
    return new Date(value).toLocaleDateString('ru-RU', {
      weekday: 'long',
      timeZone: 'Europe/Moscow',
    });
  };

  const parseDate = (value) => {
    if (!value) {
      return null;
    }
    const parsed = new Date(value);
    return Number.isNaN(parsed.getTime()) ? null : parsed;
  };

  const normalizeIconUrl = (value) => {
    if (!value) {
      return '';
    }

    if (value.startsWith('//')) {
      return `https:${value}`;
    }

    return value;
  };

  const moscowFormatter = new Intl.DateTimeFormat('en-CA', {
    timeZone: 'Europe/Moscow',
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
    hour: '2-digit',
    hourCycle: 'h23',
  });

  const toMoscowParts = (dateValue) => {
    const parts = moscowFormatter.formatToParts(dateValue);
    const getPart = (type) => Number(parts.find((part) => part.type === type)?.value);
    return {
      year: getPart('year'),
      month: getPart('month'),
      day: getPart('day'),
      hour: getPart('hour'),
    };
  };

  const toDayKey = ({ year, month, day }) => year * 10000 + month * 100 + day;

  const selectHourlyForecastWindow = (items) => {
    const now = new Date();
    const nowMoscow = toMoscowParts(now);
    const tomorrowMoscow = toMoscowParts(new Date(now.getTime() + 24 * 60 * 60 * 1000));
    const todayKey = toDayKey(nowMoscow);
    const tomorrowKey = toDayKey(tomorrowMoscow);

    return items
      .filter((item) => {
        const hourDate = parseDate(item.localTime);
        if (!hourDate) {
          return false;
        }

        const itemMoscow = toMoscowParts(hourDate);
        const itemDayKey = toDayKey(itemMoscow);

        if (itemDayKey === todayKey) {
          return itemMoscow.hour >= nowMoscow.hour;
        }

        return itemDayKey === tomorrowKey;
      })
      .sort((left, right) => new Date(left.localTime) - new Date(right.localTime));
  };

  const setHidden = (element, hidden) => {
    if (!element) {
      return;
    }

    element.classList.toggle('is-hidden', hidden);
  };

  const showLoading = (visible) => {
    setHidden(loadingSection, !visible);
  };

  const showError = (message) => {
    setHidden(errorSection, false);
    if (errorMessage) {
      errorMessage.textContent = message;
    }
    if (statusText) {
      statusText.textContent = 'Ошибка загрузки прогноза';
    }
  };

  const hideError = () => {
    setHidden(errorSection, true);
    if (errorMessage) {
      errorMessage.textContent = '';
    }
  };

  const renderHourly = (items) => {
    if (!hourlyStrip) {
      return;
    }

    if (!items.length) {
      hourlyStrip.innerHTML = '<p class="empty-forecast">Нет данных для отображения.</p>';
      return;
    }

    hourlyStrip.innerHTML = items
      .map((item) => {
        const iconUrl = normalizeIconUrl(item.conditionIconUrl);
        const icon = iconUrl
          ? `<img class="hour-icon" src="${iconUrl}" alt="${item.conditionText || 'Погода'}" loading="lazy" decoding="async" />`
          : '';

        return `
          <article class="hour-card">
            <p>${formatTime(item.localTime)}</p>
            ${icon}
            <strong>${roundTemp(item.temperatureC)}</strong>
          </article>
        `;
      })
      .join('');
  };

  const renderDaily = (items) => {
    if (!dailyList) {
      return;
    }

    dailyList.innerHTML = items
      .slice(0, 3)
      .map((day) => {
        const iconUrl = normalizeIconUrl(day.conditionIconUrl);
        const icon = iconUrl
          ? `<img class="day-icon" src="${iconUrl}" alt="${day.conditionText || 'Погода'}" loading="lazy" decoding="async" />`
          : '';

        return `
          <article class="day-row">
            <div class="day-main">
              ${icon}
              <div>
                <p class="day-name">${formatDay(day.date)}</p>
                <p class="day-condition">${day.conditionText}</p>
              </div>
            </div>
            <p class="day-temp">${roundTemp(day.minTemperatureC)} / ${roundTemp(day.maxTemperatureC)}</p>
          </article>
        `;
      })
      .join('');
  };

  const renderReport = (report) => {
    if (!report) {
      return;
    }

    if (cityEl) {
      cityEl.textContent = report.cityName || 'Москва';
    }
    if (updatedEl) {
      updatedEl.textContent = report.current?.lastUpdatedLocal || '—';
    }
    tempEl.textContent = roundTemp(report.current?.temperatureC);
    conditionEl.textContent = report.current?.conditionText || '—';
    feelsEl.textContent = `Ощущается как ${roundTemp(report.current?.feelsLikeC)}`;
    windEl.textContent = report.current?.windKph ? `${report.current.windKph} км/ч` : '—';
    humidityEl.textContent = report.current?.humidity ? `${report.current.humidity}%` : '—';
    pressureEl.textContent = report.current?.pressureMb ? `${report.current.pressureMb} мм рт. ст.` : '—';

    const currentIconUrl = normalizeIconUrl(report.current?.conditionIconUrl);
    if (currentIconEl) {
      if (currentIconUrl) {
        currentIconEl.src = currentIconUrl;
        currentIconEl.alt = report.current?.conditionText || 'Погода';
        setHidden(currentIconEl, false);
      } else {
        currentIconEl.removeAttribute('src');
        currentIconEl.alt = '';
        setHidden(currentIconEl, true);
      }
    }

    renderHourly(selectHourlyForecastWindow(report.hourlyForecast ?? []));
    renderDaily(report.dailyForecast ?? []);

    if (statusText) {
      statusText.textContent = 'Прогноз обновлён';
    }
  };

  const fetchData = async () => {
    showLoading(true);
    hideError();
    if (statusText) {
      statusText.textContent = 'Загружаем прогноз...';
    }

    try {
      const response = await fetch(endpoint);
      if (!response.ok) {
        throw new Error('Сервер вернул ошибку');
      }
      const payload = await response.json();
      renderReport(payload);
    } catch (error) {
      showError(error.message || 'Не удалось загрузить прогноз.');
    } finally {
      showLoading(false);
    }
  };

  retryButton?.addEventListener('click', fetchData);
  fetchData();
})();


