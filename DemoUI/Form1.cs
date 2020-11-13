using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Models;

namespace DemoUI
{

    public partial class Form1 : Form
    {
        ServiceCall serviceCall = null;
        public Form1()
        {
            InitializeComponent();
            SetDateTimeList();
            serviceCall = new ServiceCall();
            var data = serviceCall.SetWeekDays();
            BindData(data);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            var param = string.IsNullOrWhiteSpace(tbOptionalParam.Text) ? 0 : Convert.ToInt32(tbOptionalParam.Text);
            var data = serviceCall.ResetWeekDays(param);
            BindData(data);
        }


        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
            .Concat(controls)
            .Where(c => c.GetType() == type);
        }

        public void SetDateTimeList()
        {
            List<DateTime> list = new List<DateTime>();
            var c = GetAll(this, typeof(GroupBox));

            foreach (GroupBox gb in c)
            {
                if (gb.Text == "Post Scenario")
                {
                    var gbc = GetAll(this, typeof(DateTimePicker));
                    int i = 6;
                    foreach (DateTimePicker dt in gbc)
                    {
                        dt.Value = DateTime.Now.AddDays(i);
                        i--;
                    }
                }
            }
        }
        public List<DateTime> GetDateTimeList()
        {
            List<DateTime> list = new List<DateTime>();
            var c = GetAll(this, typeof(GroupBox));
            foreach (GroupBox gb in c)
            {
                if (gb.Text == "Post Scenario")
                {
                    var gbc = GetAll(this, typeof(DateTimePicker));
                    foreach (DateTimePicker dt in gbc)
                    {
                        list.Add(dt.Value);
                    }
                }
            }
            return list;
        }
        private void btnPost_Click(object sender, EventArgs e)
        {
            var datalist = GetDateTimeList();

            if (cbWeekDayList.DataSource != null)
            {
                try
                {
                    var lst = serviceCall.PostWeekDays(datalist);
                }
                catch(Exception ex)
                {
                    //log exception
                }
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void BindData(List<WeekDays> data)
        {

            cbWeekDayList.DisplayMember = "DayIndexDay";
            cbWeekDayList.ValueMember = "DateTimeOfDay";
            cbWeekDayList.DataSource = data;
        }
    }
}