using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELALab3Students
{
    public partial class Form1 : Form
    {
        string firstNameField;
        string lastNameField;
        string facultyField;
        int idNum;
        bool isEnable;
        StudentsDatabaseEntities studentContext;

        public Form1()
        {
            InitializeComponent();
            isEnable = true;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (isEmpty()){MessageBox.Show("Fill all fields");}
                else {
                    firstNameField = firstNameTextBox.Text.ToString();
                    lastNameField = lastNameTextBox.Text.ToString();
                    facultyField = facultyTextBox.Text.ToString();
                    idNum = Convert.ToInt32(IDTextBox.Text.ToString());
                    Student st = new Student{
                        student_id = idNum,
                        first_name = firstNameField,
                        last_name = lastNameField,
                        faculty = facultyField
                    };
                    studentContext.Students.Add(st);
                    studentContext.SaveChanges();
                    MessageBox.Show("Student added successfully.");
                    LoadToGrid();
                }
                FieldsRefresh();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string idNumString = IDTextBox.Text.ToString();
            fieldLabel.Visible = false;
            try
            {
                if (idNumString.Equals("")) { MessageBox.Show("Enter ID number to remove student"); }
                else
                {
                    int studentId = Convert.ToInt32(IDTextBox.Text.ToString());
                    Student st = studentContext.Students.First(i => i.student_id == studentId);
                    studentContext.Students.Remove(st);
                    studentContext.SaveChanges();
                    MessageBox.Show("Student deleted successfully.");
                    LoadToGrid();
                }
                FieldsRefresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            fieldLabel.Visible = false;
            LoadToGrid();
            dataGridView1.Refresh();
            FieldsRefresh();
        }

        private void LoadToGrid()
        {
            var load = from g in studentContext.Students select g;
            if (load != null)
            {
                dataGridView1.DataSource = load.ToList();
            }
        }

        public void ErrorLabels()
        {

        }

        public void FieldsRefresh()
        {
            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            facultyTextBox.Text = "";
            IDTextBox.Text = "";
        }

        public bool isEmpty()
        {
            firstNameField = firstNameTextBox.Text.ToString();
            lastNameField = lastNameTextBox.Text.ToString();
            facultyField = facultyTextBox.Text.ToString();
            string idNumString = IDTextBox.Text.ToString();

            if (firstNameField.Equals("") || lastNameField.Equals("") || facultyField.Equals("") || idNumString.Equals("")){
                fieldLabel.Visible = true;
                return true;
            }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            studentContext = new StudentsDatabaseEntities();
        }
    }
}
