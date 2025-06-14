// Copyright 2024 Genetec Inc.
// Licensed under the Apache License, Version 2.0. See the LICENSE file.

namespace Genetec.Dap.CodeSamples
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using Prism.Commands;
    using Sdk.Workspace.Components;

    public class ClockWidget : DashboardWidget, INotifyPropertyChanged
    {
        private readonly ClockWidgetView m_view;

        private bool m_showDigitalTime;

        private string m_time;

        public override Color DefaultBackgroundColor => Color.FromRgb(0, 170, 255);

        public override bool DefaultRefreshAutomatically => true;

        public override TimeSpan DefaultRefreshInterval => TimeSpan.FromMinutes(1);

        public override bool DefaultShowTitle => true;

        public override string DefaultTitle => "Clock Widget";

        public override bool IsRefreshable => true;

        public override bool IsResizableHorizontally => false;

        public override bool IsResizableVertically => false;

        public override Size MaxWidgetSize { get; } = new Size(10, 10);

        public override Size MinWidgetSize { get; } = new Size(3, 3);

        public bool ShowDigitalTime
        {
            get => m_showDigitalTime;
            set => SetProperty(ref m_showDigitalTime, value);
        }

        public string Time
        {
            get => m_time;
            set => SetProperty(ref m_time, value);
        }
        
        public ICommand ViewDigitalTimeCommand { get; }

        public override string WidgetName => "Custom Clock Widget";

        public override Size WidgetSize { get; set; } = new Size(8, 8);

        public override Guid WidgetTypeId => ClockWidgetBuilder.ClockWidgetTypeId;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        public ClockWidget()
        {
            ViewDigitalTimeCommand = new DelegateCommand(DigitalTimeCommand);
            m_view = new ClockWidgetView { DataContext = this };
        }

        public override UIElement CreateOptionsView()
        {
            return new CustomWidgetSettings { DataContext = this };
        }

        public override UIElement CreateView()
        {
            return m_view;
        }
        
        public override void Deserialize(string value)
        {
            ShowDigitalTime = ClockWidgetConfiguration.Deserialize(value).ShowDigitalTime;
        }

        public override void Refresh()
        {
            Dispatcher?.BeginInvoke(DispatcherPriority.Render, (Action)(() =>
            {
                DateTime now = DateTime.Now;
                int hourRotateValue = now.Hour;
                int minuteRotateValue = now.Minute;
                int secondRotateValue = now.Second;
                hourRotateValue = (hourRotateValue + minuteRotateValue / 60) * 30;
                minuteRotateValue = (minuteRotateValue + secondRotateValue / 60) * 6;
                m_view.MinuteRotate.Angle = minuteRotateValue;
                m_view.HourRotate.Angle = hourRotateValue;
                Time = now.ToString("h:mm tt");
            }));
        }

        public override string Serialize()
        {
            return new ClockWidgetConfiguration { ShowDigitalTime = ShowDigitalTime }.Serialize();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected override void OnWidgetEndDrag(EventArgs e)
        {
            Refresh();
        }

        private void DigitalTimeCommand()
        {
            ShowDigitalTime = !m_showDigitalTime;
            Refresh();
        }

  
    }
}