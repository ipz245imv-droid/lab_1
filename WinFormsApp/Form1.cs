using System;
using System.Windows.Forms;
using BankingLib;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private AutomatedTellerMachine atm;
        private Account? currentAccount;

        public Form1()
        {
            InitializeComponent();

            btnLogin.Click += btnLogin_Click;
            btnBalance.Click += btnBalance_Click;
            btnWithdraw.Click += btnWithdraw_Click;
            btnDeposit.Click += btnDeposit_Click;
            btnTransfer.Click += btnTransfer_Click;

            var bank = new Bank("MonoBank");
            bank.Accounts.Add(new Account("2263", "Іщенко Максим", "7009", 12000));
            bank.Accounts.Add(new Account("4444", "Мелещук Диана", "4321", 4000));

            atm = new AutomatedTellerMachine("ATM01", "Центр міста", 100000, bank);

            atm.OnAuthenticate += msg => MessageBox.Show(msg);
            atm.OnBalanceCheck += msg => MessageBox.Show(msg);
            atm.OnWithdraw += msg => MessageBox.Show(msg);
            atm.OnDeposit += msg => MessageBox.Show(msg);
            atm.OnTransfer += msg => MessageBox.Show(msg);

            SetUiLoggedIn(false);
        }

        private void SetUiLoggedIn(bool isLoggedIn)
        {
            btnBalance.Enabled = isLoggedIn;
            btnWithdraw.Enabled = isLoggedIn;
            btnDeposit.Enabled = isLoggedIn;
            btnTransfer.Enabled = isLoggedIn;

            txtAmount.Enabled = isLoggedIn;
            txtTargetCard.Enabled = isLoggedIn;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            currentAccount = atm.Authenticate(txtCard.Text, txtPin.Text);
            SetUiLoggedIn(currentAccount != null);
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            if (currentAccount == null)
            {
                MessageBox.Show("Спочатку виконайте авторизацію.");
                return;
            }
            atm.CheckBalance(currentAccount);
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (currentAccount == null)
            {
                MessageBox.Show("Спочатку виконайте авторизацію.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out var amount))
            {
                MessageBox.Show("Некоректна сума.");
                return;
            }

            atm.Withdraw(currentAccount, amount);
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (currentAccount == null)
            {
                MessageBox.Show("Спочатку виконайте авторизацію.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out var amount))
            {
                MessageBox.Show("Некоректна сума.");
                return;
            }

            atm.Deposit(currentAccount, amount);
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (currentAccount == null)
            {
                MessageBox.Show("Спочатку виконайте авторизацію.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out var amount))
            {
                MessageBox.Show("Некоректна сума.");
                return;
            }

            atm.Transfer(currentAccount, txtTargetCard.Text, amount);
        }
    }
}
