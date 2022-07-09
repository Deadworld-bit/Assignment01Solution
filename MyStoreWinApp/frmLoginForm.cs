using BussinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmLoginForm : Form
    {
        public frmLoginForm()
        {
            InitializeComponent();
        }
        public IMemberRepository MemberRepository { get; set; }
        public MemberObject MemberInfo { get; set; }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            int memberid = int.Parse(txtMemberId.Text);
            string password = txtPassword.Text;
            try
            {
                MemberInfo = MemberRepository.Login(memberid, password);
                if (MemberInfo != null)
                {
                    if (memberid.Equals("1"))
                    {
                        Program.manageform = new frmMemberManagement();
                        Program.manageform.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("You are not allowed to access this function!");
                    }
                }
                else
                {
                    MessageBox.Show("Wrong UserID or Password !");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => ResetText();

        private void btnExit_Click(object sender, EventArgs e) => Close();
    }
}
