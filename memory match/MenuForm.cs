namespace memory_match {
    public partial class MenuForm : Form {
        public MenuForm() {
            InitializeComponent();
        }

        private void btnStartGame_Click(object sender, EventArgs e) {
            this.Hide();
            var cardPairs = (int)noCardPairs.Value;
            var gameForm = new GameForm(cardPairs);
            gameForm.Closed += (s, args) => this.Close();
            gameForm.Show();
        }
    }
}
