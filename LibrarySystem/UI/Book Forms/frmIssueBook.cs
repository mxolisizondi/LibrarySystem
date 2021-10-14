using LibrarySystem.DataAccessLayer;
using LibrarySystem.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace LibrarySystem.UI.Book_Forms
{
    public partial class frmIssueBook : DockContent
    {
        private int _staffID;
        public frmIssueBook(int staffID)
        {
            InitializeComponent();
            _staffID = staffID;
        }

        private void FrmIssueBook_Load(object sender, EventArgs e)
        {
            dtpReturnDate.MinDate = dtpIssueDate.Value.AddDays(1);
        }

        private void FillGridStudent(Students s)
        {
            dgvStudentDetail.Rows.Clear();
            dgvStudentDetail.Rows.Add($"Name   : {s.Name}");
            dgvStudentDetail.Rows.Add($"TC No : {s.TCNO}");
            dgvStudentDetail.Rows.Add($"Session : {SessionsHelper.GetByNameFromID(s.SessionID)}");
            dgvStudentDetail.Rows.Add($"Department : {DepartmentsHelper.GetByNameFromID(s.DepartmentID)}");
            dgvStudentDetail.Rows.Add($"Program : {ProgramsHelper.GetByNameFromID(s.ProgramID)}");
        }

        private void FillGridBook(Books b)
        {
            dgvBookDetail.Rows.Clear();
            dgvBookDetail.Rows.Add($"Book name       : {b.BookName}");
            dgvBookDetail.Rows.Add($"Author    : {b.Author}");
            dgvBookDetail.Rows.Add($"Category  : {BookCategoriesHelper.GetByNameFromID(b.BookCategoryID)}");
            dgvBookDetail.Rows.Add($"Department     : {DepartmentsHelper.GetByNameFromID(b.DepartmentID)}");
            dgvBookDetail.Rows.Add($"No of Copies    : {b.NoOfCopies}");
        }

        private void TxtStudentID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnSearchStudent_Click(object sender, EventArgs e)
        {
            
            ep.Clear();
            if (txtStudentID.Text.Trim().Length == 0)
            {
                ep.SetError(txtStudentID, "Enter your student ID!");
                txtStudentID.Focus();
                return;
            }

            try
            {
                var s = StudentsHelper.GetStudentByID(Convert.ToInt32(txtStudentID.Text));
                FillGridStudent(s);
                btnSearchStudent.Enabled = false;
                txtStudentID.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Student with this ID not found", "Library Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (btnSearchBook.Enabled == false || btnSearchStudent.Enabled == false)
            {
                btnCancel.Enabled = true;
            }
            if (btnSearchBook.Enabled == false && btnSearchStudent.Enabled == false)
            {
                btnAdd.Enabled = true;
            }
        }

        private void TxtBookID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnSearchBook_Click(object sender, EventArgs e)
        {
            ep.Clear();
            if (txtBookID.Text.Trim().Length == 0)
            {
                ep.SetError(txtBookID, "Enter book id");
                txtBookID.Focus();
                return;
            }

            try
            {
                var b = BooksHelper.GetBookByID(Convert.ToInt32(txtBookID.Text));
                if (b.NoOfCopies > 0)
                {
                    FillGridBook(b);
                    btnSearchBook.Enabled = false;
                    txtBookID.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Book not found.", "Library Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Book does not exists", "Library Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (btnSearchBook.Enabled == false || btnSearchStudent.Enabled == false)
            {
                btnCancel.Enabled = true;
            }
            if (btnSearchBook.Enabled == false && btnSearchStudent.Enabled == false)
            {
                btnAdd.Enabled = true;
            }
        }

        private void DtpIssueDate_ValueChanged(object sender, EventArgs e)
        {
            dtpReturnDate.MinDate = dtpIssueDate.Value.AddDays(1);
        }

        private void btnAddDgv_Click(object sender, EventArgs e)
        {
            if (btnSearchStudent.Enabled==false && btnSearchBook.Enabled == false)
            {
                if (FinesHelper.GetFineCount(Convert.ToInt32(txtStudentID.Text)) < 1)
                {
                    if (!IssueBooksHelper.GetHaveBooks(Convert.ToInt32(txtStudentID.Text), Convert.ToInt32(txtBookID.Text)))
                    {
                        IssueBooks ib = new IssueBooks();
                        ib.StudentID = Convert.ToInt32(txtStudentID.Text);
                        ib.BookID = Convert.ToInt32(txtBookID.Text);
                        ib.StaffID = _staffID;
                        ib.NoOfCopies = 1;
                        ib.DateOfIssue = dtpIssueDate.Value;
                        ib.DateOfReturn = dtpReturnDate.Value;
                        ib.Status = 1;
                        IssueBooksHelper.Add(ib);

                        var b = BooksHelper.GetBookByID(Convert.ToInt32(txtBookID.Text));
                        b.NoOfCopies -= 1;
                        BooksHelper.Update(b);

                        MessageBox.Show("Book rental successful!", "Library Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        EnableComponent();
                    }
                    else
                    {
                        MessageBox.Show("A Student Is Not Likely To Buy The Same Book More Than One.", "Library Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No Opportunity to Rent Books Since the Student Has Multiple Unpaid Debts.", "Library Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please choose student and book!", "Library Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            EnableComponent();
        }

        private void EnableComponent()
        {
            txtStudentID.Text = string.Empty;
            txtStudentID.Enabled = true;

            txtBookID.Text = string.Empty;
            txtBookID.Enabled = true;

            dgvStudentDetail.Rows.Clear();
            dgvBookDetail.Rows.Clear();

            btnCancel.Enabled = false;
            btnAdd.Enabled = false;

            btnSearchStudent.Enabled = true;
            btnSearchBook.Enabled = true;
        }
    }
}
