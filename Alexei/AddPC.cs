namespace Alexei
{
    public partial class AddPC : Form
    {
        Team team;
        bool add;
        public AddPC(Team team,bool add)
        {
            this.team = team;
            this.add = add;
            InitializeComponent();
        }

        private void AddPC_Load(object sender, EventArgs e)
        {
            if(add)
            {
                this.Text = "Add Players";
                label1.Text = "Surname Player";
                label2.Text = "Age Player";
                label3.Text = "Number Player";
                button1.Visible = true;
                button2.Visible = false;
            }
            else
            {
                this.Text = "Add Coach";
                label1.Text = "Surname Coach";
                label2.Text = "Age Coach";
                label3.Text = "Stag Coach";
                button2.Visible = true;
                button1.Visible = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1.Text) 
                && !string.IsNullOrEmpty(textBox2.Text)
                && !string.IsNullOrEmpty(textBox3.Text))
            {
                if(textBox1.Text.Length > 2)
                {
                    try
                    {
                        int age = int.Parse(textBox2.Text);
                        int number = int.Parse(textBox3.Text);
                        if (age > 0 && age < 70 && number > 0) {
                            Player pl = new Player();
                            pl.Surname = textBox1.Text;
                            pl.Age = age;
                            pl.Number = number;
                            using (MyBD bd = new MyBD())
                            {
                                var tm = bd.Team.Where(t => t.Name == team.Name).FirstOrDefault();
                                pl.Team = tm;
                                bd.Player.Add(pl);
                                bd.Entry(tm).State = System.Data.Entity.EntityState.Modified;
                                bd.SaveChanges();

                            }
                            MessageBox.Show("Succes");
                        }
                        else MessageBox.Show("Incorrect");
                    }
                    catch
                    {
                        MessageBox.Show("Incorrect");
                    }

                }
            }
            else
            {
                MessageBox.Show("Incorrect");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text)
                && !string.IsNullOrEmpty(textBox2.Text)
                && !string.IsNullOrEmpty(textBox3.Text))
            {
                if (textBox1.Text.Length > 2)
                {
                    try
                    {
                        int age = int.Parse(textBox2.Text);
                        int stag = int.Parse(textBox3.Text);
                        if (age > stag && age > 0 && age < 120 && age - stag > 14)
                        {
                            Coach pl = new Coach();
                            pl.Surname = textBox1.Text;
                            pl.Age = age;
                            pl.Expirience = stag;
                            using (MyBD bd = new MyBD())
                            {
                                var tm = bd.Team.Where(t => t.Name == team.Name).FirstOrDefault();
                                pl.Team = tm;
                                bd.Coach.Add(pl);
                                bd.Entry(tm).State = System.Data.Entity.EntityState.Modified;
                                bd.SaveChanges();

                            }
                            MessageBox.Show("Succes");
                        }
                        else
                        {
                            MessageBox.Show("Incorrect");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Incorrect");
                    }

                }
            }
            else
            {
                MessageBox.Show("Incorrect");
            }
        }
    }
}
