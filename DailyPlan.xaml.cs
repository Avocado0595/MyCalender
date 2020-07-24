using System;
using System.Windows;
using System.Windows.Controls;

namespace MyCalender
{
    /// <summary>
    /// Interaction logic for DailyPlan.xaml
    /// </summary>
    public partial class DailyPlan : Window
    {
        private DateTime date;
        private PlanData job;
        public DateTime Date { get => date; set => date = value; }
        public PlanData Job { get => job; set => job = value; }

        public DailyPlan(DateTime date, PlanData job)
        {
            InitializeComponent();
            this.Date = date;
            this.Job = job;
            dtpSelectionDate.SelectedDate = Date;
        }

        private void ShowJobByDate(DateTime date)
        {
            spnlPlans.Children.Clear();
            if (Job != null && Job.Job != null)
            {
                for (int i = 0; i < Job.Job.Count; i++)
                {
                    if (Job.Job[i].Date.Date == date.Date)
                    {
                        Addjob(Job.Job[i]);
                    }
                }
            }
        }
        void Addjob(PlanItem job)
        {
            Ajob ajob = new Ajob(job);
            ajob.Edit += Ajob_Edit;
            ajob.Delete += Ajob_Delete;
            spnlPlans.Children.Add(ajob);
        }
        private void Ajob_Delete(object sender, EventArgs e)
        {
            Ajob uc = sender as Ajob;
            PlanItem job = uc.Job;
            
            spnlPlans.Children.Remove(uc);
            Job.Job.Remove(job);
        }

        private void Ajob_Edit(object sender, EventArgs e)
        {
            
        }

        private void dtpSelectionDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowJobByDate((sender as DatePicker).SelectedDate.Value);
        }

        private void btnPrevday_Click(object sender, RoutedEventArgs e)
        {
            dtpSelectionDate.SelectedDate = dtpSelectionDate.SelectedDate.Value.AddDays(-1);
        }

        private void btnNextday_Click(object sender, RoutedEventArgs e)
        {
            dtpSelectionDate.SelectedDate = dtpSelectionDate.SelectedDate.Value.AddDays(1);
        }

        private void MenuItemToday_Click(object sender, RoutedEventArgs e)
        {
            dtpSelectionDate.SelectedDate = DateTime.Now;
        }

        private void MenuItemAddJob_Click(object sender, RoutedEventArgs e)
        {

            PlanItem item = new PlanItem() { Date = dtpSelectionDate.SelectedDate.Value, FromTime = new Point(0,0), ToTime=new Point(0,0), Job = "", Status="COMING" };
            Job.Job.Add(item);
            Addjob(item);
        }
    }
}
