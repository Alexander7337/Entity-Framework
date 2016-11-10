using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace HospitalSystem.UI
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using HospitalSystem.Data;

    public partial class MainWindow : Window
    {
        private string firstName = null;
        private string lastName = null;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var context = new HospitalContext();
            var patient = context.Patients
                .Where(n => n.FirstName == this.firstName
                                    && n.LastName == this.lastName)
                .Select(p => new
                {
                    p.FirstName,
                    p.LastName,
                    p.Email,
                    p.DateOfBirth,
                    p.HasInsurance,
                    Diagnose = p.Diagnoses.FirstOrDefault().Name,
                    Visitation = p.Visitations.FirstOrDefault().Date
                })
                .ToList();

            MyDataGrid.ItemsSource = patient;

            #region Bind data via SqlDataAtapter
            //var connection = context.Database.Connection.ConnectionString;

            //string query = "SELECT * FROM dbo.PATIENTS";
            //DataSet data = new DataSet();
            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
            //sqlDataAdapter.Fill(data, "Patients");
            //MyDataGrid.ItemsSource = data.Tables["Patients"].DefaultView;
            #endregion
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            FirstName.Text = String.Empty;
            LastName.Text = String.Empty;
        }

        private void FirstName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBoxInput = (TextBox)sender;
            this.firstName = textBoxInput.Text;
        }

        private void LastName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBoxInput = (TextBox)sender;
            this.lastName = textBoxInput.Text;
        }
    }
}
