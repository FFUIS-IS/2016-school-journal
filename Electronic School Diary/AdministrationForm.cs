﻿using ElectronicSchoolDiary.Models;
using ElectronicSchoolDiary.Repos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronicSchoolDiary
{
    public partial class AdministrationForm : Form
    {
        private User CurrentUser;
        private Admin CurrentAdmin;

        public void warning()
        {
            MessageBox.Show("Polja ne mogu biti prazna !");
        }
        public AdministrationForm(User user, Admin admin)
        {
            InitializeComponent();
            this.Text = "Administrator : " + admin.Name + " " + admin.Surname;
            CurrentUser = user;
            CurrentAdmin = admin;
        }
        private void AdministrationForm_Load(object sender, EventArgs e)
        {
            CenterToParent();
            ControlBox = false;
            UserBox.SelectedIndex = 2;
            ClassNumberComboBox.SelectedIndex = 0;
            PopulateDepartmentsComboBox();
            PopulateSectionsComboBox();
            PopulateClassNumberComboBox();
            PopulateClassNumbComboBox();
            PopulateTeachersComboBox();
        }

        private void PopulateDepartmentsComboBox()
        {
                string DepartmentQuery =  DepartmentsRepository.GetQuery();
                string SectionsQuery = SectionsRepository.GetQuery();
                Lists.FillDropDownList2(SectionsQuery,"Description",DepartmentQuery,"Title",DepartmentComboBox);
        }
        private void PopulateSectionsComboBox()
        {
                string SectionsQuery = SectionsRepository.GetQuery();
                Lists.FillDropDownList1(SectionsQuery, "Description", SectionsComboBox);
        }
        private void PopulateClassNumberComboBox()
        {
            string ClassesQuery = ClassesRepository.GetQuery();
            Lists.FillDropDownList1(ClassesQuery,"Number", ClassNumberComboBox);
        }
        private void PopulateClassNumbComboBox()
        {
            string ClassesQuery = ClassesRepository.GetQuery();
            Lists.FillDropDownList1(ClassesQuery, "Number", ClassComboBox);
        }
        private void PopulateCoursesListBox()
        {
            string CoursesQuery = CoursesRepository.GetQuery();
            Lists.FillCheckedListBox(CoursesQuery, CoursesCheckedListBox);
        }
        private void PopulateTeachersComboBox()
        {
            string Name = TeacherRepository.GetNameQuery();
            string Surname = TeacherRepository.GetSurnameQuery();
            Lists.FillDropDownList2(Name, "Name", Surname, "Surname", ClassTeacherComboBox);
        }

        private string selectedUser()
        {
            return UserBox.Text;
        }
        private void UserBox_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            try
            {
                string current = selectedUser();
              if(current == "Administratora")
                {
                    AdministratorPanel.Show();
                    label2.Show();
                    UserBox.Show();
                    StudentPanel.Hide();
                    TeachersPanel.Hide();
                    PasswordPanel.Hide();
                    DepartmentPanel.Hide();
                }
            if (current == "Nastavnika")
            {
                TeachersPanel.Show();
                label2.Show();
                UserBox.Show();
                StudentPanel.Hide();
                AdministratorPanel.Hide();
                PasswordPanel.Hide();
                DepartmentPanel.Hide();
            }
            if (current == "Ucenika i roditelja")
            {
                StudentPanel.Show();
                label2.Show();
                UserBox.Show();
                TeachersPanel.Hide();
                AdministratorPanel.Hide();
                PasswordPanel.Hide();
                DepartmentPanel.Hide();
            }
           
                if (current == "Odjeljenja i razrednici")
                {
                    DepartmentPanel.Show();
                    label2.Show();
                    UserBox.Show();
                    StudentPanel.Hide();
                    TeachersPanel.Hide();
                    AdministratorPanel.Hide();
                    PasswordPanel.Hide();
                }


            }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }

        }

        private void UserSettingsButton_Click(object sender, EventArgs e)
        {
            UserBox.SelectedIndex = 2;
            PasswordPanel.Show();
            StudentPanel.Hide();
            TeachersPanel.Hide();
            AdministratorPanel.Hide();
            DepartmentPanel.Hide();
            label2.Hide();
            UserBox.Hide();

        }

        private void LogOutUserButton_Click(object sender, EventArgs e)
        {
          
                Form form = new LoginForm();
                form.Show();
                this.Close();
         
        }

        private void AddAdminButton_Click(object sender, EventArgs e)
        {
            if (AdminNameTextBox.Text.Length == 0 || AdminSurnameTextBox.Text.Length == 0 || UserNameTextBox.Text.Length == 0)
            {
                warning();
            }
            else
            {
                bool isUnique = UsersRepository.CheckUnique(UserNameTextBox.Text);
                if(isUnique != true)
                {
                    bool isAdminAdded = AdminRepository.AddAdmin(AdminNameTextBox.Text, AdminSurnameTextBox.Text, UserNameTextBox.Text, AdminNameTextBox.Text );
                    if (isAdminAdded == true)
                    {
                        AdminNameTextBox.Text = "";
                        AdminSurnameTextBox.Text = "";
                        UserNameTextBox.Text = "";
                    }
                    else
                    {
                        AdminNameTextBox.Text = "";
                        AdminSurnameTextBox.Text = "";
                        UserNameTextBox.Text = "";
                    }
                }
            }
        }
        private void AddSectionsButton_Click(object sender, EventArgs e)
        {
            if (SectionsNameTextBox.Text.Length == 0) { warning(); }
            else
            {
                bool isUnique = SectionsRepository.CheckUnique(SectionsNameTextBox.Text);
                if (isUnique != true)
                {
                    bool isSectionAdded = SectionsRepository.InsertSection(SectionsNameTextBox.Text);
                    if (isSectionAdded == true)
                    {
                        SectionsNameTextBox.Text = "";
                        PopulateSectionsComboBox();
                    }
                }
                else SectionsNameTextBox.Text = "";

            }
        }
     
        private void AddDirectorButton_Click(object sender, EventArgs e)
        {
            if (DirectorNameTextBox.Text.Length == 0 || DirectorSurnameTextBox.Text.Length == 0 || DirectorUserNameTextBox.Text.Length == 0)
            {
                warning();
            }
            else
            {
                bool isUnique = UsersRepository.CheckUnique(DirectorUserNameTextBox.Text);
                if (isUnique != true)
                {
                    bool isDirectorAdded = DirectorRepository.AddDirector(DirectorNameTextBox.Text, DirectorSurnameTextBox.Text, DirectorUserNameTextBox.Text, DirectorNameTextBox.Text);
                    if (isDirectorAdded == true)
                    {
                        DirectorNameTextBox.Text = "";
                        DirectorSurnameTextBox.Text = "";
                        DirectorUserNameTextBox.Text = "";
                    }
                    else
                    {
                        DirectorNameTextBox.Text = "";
                        DirectorSurnameTextBox.Text = "";
                        DirectorUserNameTextBox.Text = "";
                    }
                }
               
            }
        }

        private void ChangePassAdminButton_Click(object sender, EventArgs e)
        {

            if (OldPassTextBox.Text.Length == 0 ||
                NewPassTextBox.Text.Length == 0 ||
                ConfirmedNewPassTextBox.Text.Length == 0)
            {
                warning();
            }
            else
            {
                if (OldPassTextBox.Text == NewPassTextBox.Text && OldPassTextBox.Text == CurrentUser.Password)
                {
                    MessageBox.Show("Unesite novu lozinku koja se razlikuje od stare !");
                }
                else if (OldPassTextBox.Text == CurrentUser.Password && NewPassTextBox.Text == ConfirmedNewPassTextBox.Text)
                {
                   bool isChanged =  UsersRepository.ChangePassword(CurrentUser.Id, OldPassTextBox.Text, NewPassTextBox.Text, ConfirmedNewPassTextBox.Text);
                    if (isChanged == true)
                    {
                        StudentPanel.Show();
                        label2.Show();
                        UserBox.Show();
                        TeachersPanel.Hide();
                        AdministratorPanel.Hide();
                        DepartmentPanel.Hide();
                        PasswordPanel.Hide();
                        UserSettingsButton.Hide();
                    }
                    
                }
                else if(NewPassTextBox.Text != ConfirmedNewPassTextBox.Text)
                MessageBox.Show("Nove lozinke se ne poklapaju !");
                else MessageBox.Show("Pogrešna lozinka !");

            }
        }

        private void AddStudentButton_Click(object sender, EventArgs e)
        {
            if (StudentNameTextBox.Text.Length == 0 ||
            StudentSurnameTextBox.Text.Length == 0 ||
            StudentJmbgTextBox.Text.Length == 0 ||
            ParentNameTextBox.Text.Length == 0||
            ParentSurnameTextBox.Text.Length == 0)
            {
                warning();
            }
            else
            {
                bool isStudentAdded = StudentRepository.AddStudent(StudentNameTextBox.Text, StudentSurnameTextBox.Text, StudentJmbgTextBox.Text, StudentAddressTextBox.Text, StudentPhoneTextBox.Text);
                if (isStudentAdded == true)
                {
                    StudentNameTextBox.Text = "";
                    StudentSurnameTextBox.Text = "";
                    StudentJmbgTextBox.Text = "";
                    StudentAddressTextBox.Text = "";
                    StudentPhoneTextBox.Text = "";
                    DepartmentComboBox.Text = "";
                }
                bool isParentAdded = ParentRepository.AddParent(ParentNameTextBox.Text, ParentSurnameTextBox.Text, ParentAddressTextBox.Text, ParentEmailTextBox.Text, ParentPhoneTextBox.Text, StudentJmbgTextBox.Text); 
                if(isParentAdded == true)
                {

                    ParentNameTextBox.Text = "";
                    ParentSurnameTextBox.Text = "";
                    ParentAddressTextBox.Text = "";
                    ParentEmailTextBox.Text = "";
                    ParentPhoneTextBox.Text = "";
                    StudentJmbgTextBox.Text = "";
                }
            }
        }
        private void ControlTableButton_Click(object sender, EventArgs e)
        {
            StudentPanel.Show();
            label2.Show();
            UserBox.Show();
            TeachersPanel.Hide();
            AdministratorPanel.Hide();
            DepartmentPanel.Hide();
            PasswordPanel.Hide();
        }

        private void AddCourseButton_Click(object sender, EventArgs e)
        {
            if(CourseTextBox.Text.Length == 0)
            {
                warning();
            }
        }

        private void AddDepartmentAndClassTeacherButton_Click(object sender, EventArgs e)
        {
            if(StudentNameTextBox.Text.Length == 0 ||
               StudentSurnameTextBox.Text.Length == 0 ||
               StudentNameTextBox.Text.Length == 0 ||
               StudentJmbgTextBox.Text.Length == 0 ||
               ParentNameTextBox.Text.Length == 0 ||
               ParentSurnameTextBox.Text.Length == 0 ||
               StudentNameTextBox.Text.Length == 0
                )
            {
                warning();
            }
        }

        private void AddTeacherButton_Click(object sender, EventArgs e)
        {
            if (TeacherNameTextBox.Text.Length == 0 ||
                TeacherSurnameTextBox.Text.Length == 0 ||
                TeacherUserNameTextBox.Text.Length == 0)
            {
                warning();
            }
            else
            {
                bool isUnique = UsersRepository.CheckUnique(TeacherUserNameTextBox.Text);
                if (isUnique != true)
                {
                    bool isTeacherAdded = TeacherRepository.AddTeacher(TeacherNameTextBox.Text, TeacherSurnameTextBox.Text, TeacherUserNameTextBox.Text, TeacherAddressTextBox.Text, TeacherPhoneTextBox.Text, TeacherNameTextBox.Text);
                    if (isTeacherAdded == true)
                    {
                        TeacherNameTextBox.Text = "";
                        TeacherSurnameTextBox.Text = "";
                        TeacherUserNameTextBox.Text = "";
                        TeacherAddressTextBox.Text = "";
                        TeacherPhoneTextBox.Text = "";
                    }
                }
                else
                {
                    TeacherNameTextBox.Text = "";
                    TeacherSurnameTextBox.Text = "";
                    TeacherUserNameTextBox.Text = "";
                    TeacherAddressTextBox.Text = "";
                    TeacherPhoneTextBox.Text = "";
                }
            }
        }
    }
}