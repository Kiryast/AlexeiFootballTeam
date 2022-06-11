namespace Alexei
{
    public partial class Form1 : Form
    {
        public Team team = new Team();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            using (MyBD db = new MyBD())
            {
                try
                {
                    var list = db.Team.ToList();
                    if (list.Count > 0)
                        foreach (var item in list)
                            comboBox1.Items.Add(item.Name);
                }
                catch
                {
                    MessageBox.Show("Нет комманд");
                }
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddTeam form = new AddTeam();
            form.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                var teamname = comboBox1.Text;
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.AddRange(
                    new DataGridViewTextBoxColumn() { Name = "zName", HeaderText = "Фамилия" },
                    new DataGridViewTextBoxColumn() { Name = "zDescription", HeaderText = "Возраст" },
                    new DataGridViewTextBoxColumn() { Name = "zDate", HeaderText = "Номер" });
                dataGridView2.Columns.Clear();
                dataGridView2.Columns.AddRange(
                    new DataGridViewTextBoxColumn() { Name = "zName", HeaderText = "Фамилия" },
                    new DataGridViewTextBoxColumn() { Name = "zDescription", HeaderText = "Возраст" },
                    new DataGridViewTextBoxColumn() { Name = "zDate", HeaderText = "Стаж" });
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dataGridView1.Columns.Add(btn);
                btn.HeaderText = "Удаление";
                btn.Text = "Удалить";
                btn.Name = "btn";
                btn.UseColumnTextForButtonValue = true;
                DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
                dataGridView2.Columns.Add(btn2);
                btn2.HeaderText = "Удаление";
                btn2.Text = "Удалить";
                btn2.Name = "btn";
                btn2.UseColumnTextForButtonValue = true;
                dataGridView1.RowHeadersVisible = false;
                dataGridView2.RowHeadersVisible = false;
                using (MyBD db = new MyBD())
                {
                    try
                    {
                        var team = db.Team.Where(t => t.Name == teamname).First();
                        countyteam.Text = "Team from " + team.Country;
                        this.team = team;
                        var listplayer = db.Player.Where(p => p.TeamId == team.Id).ToList();
                        if (listplayer.Count > 0)
                            for (int i = 0; i < listplayer.Count; i++)
                            {
                                dataGridView1.Rows.Add();
                                dataGridView1[0, i].Value = listplayer[i].Surname;
                                dataGridView1[1, i].Value = listplayer[i].Age.ToString();
                                dataGridView1[2, i].Value = listplayer[i].Number.ToString();
                            }
                        var listcoach = db.Coach.Where(p => p.TeamId == team.Id).ToList();
                        if (listcoach.Count > 0)
                            for (int i = 0; i < listcoach.Count; i++)
                            {
                                dataGridView2.Rows.Add();
                                dataGridView2[0, i].Value = listcoach[i].Surname;
                                dataGridView2[1, i].Value = listcoach[i].Age.ToString();
                                dataGridView2[2, i].Value = listcoach[i].Expirience.ToString();
                            }
                    }
                    catch { }
                }
            }
            else
            {
                MessageBox.Show("Select team");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                AddPC form = new AddPC(team, true);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select team");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text)) 
            {
                AddPC form = new AddPC(team, false);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select team");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Form1_Load(sender, e);
            this.comboBox1_SelectedIndexChanged(sender, e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                    using (MyBD db = new MyBD())
                    {
                        var list = db.Player.Where(p => p.TeamId == team.Id).ToList();
                        Player pl = list[e.RowIndex];
                        var tt = db.Team.Where(t => t.Id == team.Id).First();
                        db.Player.Remove(pl);
                        db.Entry(tt).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                        comboBox1_SelectedIndexChanged(sender, e);

                    }
            }
            catch
            {

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                    using (MyBD db = new MyBD())
                    {
                        var list = db.Coach.Where(p => p.TeamId == team.Id).ToList();
                        Coach pl = list[e.RowIndex];
                        var tt = db.Team.Where(t => t.Id == team.Id).First();
                        db.Coach.Remove(pl);
                        db.Entry(tt).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                        comboBox1_SelectedIndexChanged(sender, e);

                    }
            }
            catch
            {

            }
        }
    }
}