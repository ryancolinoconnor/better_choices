using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Better_Choices_1.Analytics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Forecast : ContentView
    {
        public Forecast()
        {
            InitializeComponent();
            SfChart chart = new SfChart();
            chart.Title.Text = "Cumulative savings";

            //Initializing primary axis
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Title.Text = "Date";
            chart.PrimaryAxis = primaryAxis;

            //Initializing secondary Axis
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Title.Text = "Savings";
            chart.SecondaryAxis = secondaryAxis;

            ViewDates view = new ViewDates();
            //Initializing column series
            ColumnSeries series = new ColumnSeries();
            series.ItemsSource = view.Data;
            series.XBindingPath = "Date";
            series.YBindingPath = "value";
            series.Label = "Dollars";

            series.DataMarker = new ChartDataMarker();
            series.EnableTooltip = true;
            chart.Legend = new ChartLegend();

            chart.Series.Add(series);
            this.Content = chart;
        }
        public Forecast(string filter,DateTime start_date,DateTime end_date)
        {
            InitializeComponent();
            SfChart chart = new SfChart();
            chart.Title.Text = "Cumulative savings";

            //Initializing primary axis
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Title.Text = "Date";
            chart.PrimaryAxis = primaryAxis;

            //Initializing secondary Axis
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Title.Text = "Savings";
            chart.SecondaryAxis = secondaryAxis;

            ViewDates view = new ViewDates(filter, start_date,end_date);



            //Initializing column series
            ColumnSeries series = new ColumnSeries();
            series.ItemsSource = view.Data;
            series.XBindingPath = "Date";
            series.YBindingPath = "value";
            series.Label = "Dollars";

            series.DataMarker = new ChartDataMarker();
            series.EnableTooltip = true;
            chart.Legend = new ChartLegend();

            chart.Series.Add(series);
            double Y1 = App.Database.GetStashTarget(filter);
            if (Y1 != 0) {
                HorizontalLineAnnotation horizontal = new HorizontalLineAnnotation()
                {
                    Y1 = Y1
                };

                chart.SecondaryAxis = new NumericalAxis() { Minimum = 10, Maximum = Y1 + 10 };
                chart.ChartAnnotations.Add(horizontal);
            }
            this.Content = chart;
        }
    }
}