using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alexei
{
    public partial class AddTeam : Form
    {
        List<Team> teamList = new List<Team>();
        public AddTeam()
        {
            InitializeComponent();
        }
        private void AddTeam_Load(object sender, EventArgs e)
        {
            using (MyBD bd = new MyBD())
            {
                try
                {
                    var tl = bd.Team.ToList();
                    if (tl.Count > 0)
                        foreach (var item in tl)
                            teamList.Add(item);
                }
                catch { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(textBox1.Text) 
                && !String.IsNullOrEmpty(textBox2.Text) 
                && textBox1.Text.Length > 5 
                && textBox2.Text.Length > 3)
            {
                bool temp = true;
                foreach (var item in teamList)
                    if (item.Name == textBox1.Text)
                    {
                        temp = false;
                        break;
                    }
                if (temp)
                {
                    Team tempteam = new Team();
                    tempteam.Name = textBox1.Text;
                    tempteam.Country = textBox2.Text;
                    using (MyBD db = new MyBD())
                    {
                        db.Team.Add(tempteam);
                        db.SaveChanges();
                    }
                    MessageBox.Show("Succes");
                }
                else MessageBox.Show("Takoi team sushestwuet");
            }
        }


    }
}
