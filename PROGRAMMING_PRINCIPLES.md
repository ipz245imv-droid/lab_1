# Programming Principles

---

## 1. DRY (Don't Repeat Yourself)

**Опис:** Кожна частина знань повинна мати єдине, однозначне представлення в системі.

**Демонстрація:**

Метод [`SetUiLoggedIn`](https://github.com/ipz245imv-droid/lab_1/blob/master/WinFormsApp/Form1.cs#L41-L50) централізує керування станом кнопок, уникаючи дублювання:

```csharp
private void SetUiLoggedIn(bool isLoggedIn)
{
    btnBalance.Enabled = isLoggedIn;
    btnWithdraw.Enabled = isLoggedIn;
    btnDeposit.Enabled = isLoggedIn;
    btnTransfer.Enabled = isLoggedIn;
    txtAmount.Enabled = isLoggedIn;
    txtTargetCard.Enabled = isLoggedIn;
}
```

Завдяки цьому стан UI змінюється в одному місці — і після логіну, і після помилки автентифікації.

---

## 2. KISS (Keep It Simple, Stupid)

**Опис:** Простота повинна бути ключовою метою в проєктуванні. Слід уникати зайвої складності.

**Демонстрація:**

Метод [`FindAccount`](https://github.com/ipz245imv-droid/lab_1/blob/master/BankingLib/Bank.cs#L11-L12) у `Bank.cs` реалізовано максимально просто — в один рядок:

```csharp
public Account? FindAccount(string cardNumber)
    => Accounts.FirstOrDefault(a => a.CardNumber == cardNumber);
```

Метод виконує рівно одну задачу і не містить зайвих абстракцій.

---

## 3. SRP — Single Responsibility Principle

**Опис:** Кожен клас повинен мати лише одну відповідальність.

**Демонстрація:**

- [`Account.cs`](https://github.com/ipz245imv-droid/lab_1/blob/master/BankingLib/Account.cs) — відповідає виключно за дані рахунку та операції з балансом.
- [`Bank.cs`](https://github.com/ipz245imv-droid/lab_1/blob/master/BankingLib/Bank.cs) — відповідає виключно за зберігання та пошук рахунків.
- [`AutomatedTellerMachine.cs`](https://github.com/ipz245imv-droid/lab_1/blob/master/BankingLib/AutomatedTellerMachine.cs) — відповідає виключно за логіку банкомату.
- [`Form1.cs`](https://github.com/ipz245imv-droid/lab_1/blob/master/WinFormsApp/Form1.cs) — відповідає виключно за UI та взаємодію з користувачем.

---

## 4. OCP — Open/Closed Principle

**Опис:** Клас повинен бути відкритим для розширення, але закритим для модифікації.

**Демонстрація:**

[`AutomatedTellerMachine`](https://github.com/ipz245imv-droid/lab_1/blob/master/BankingLib/AutomatedTellerMachine.cs#L13-L18) використовує події для сповіщень — нову поведінку можна додати підпискою, не змінюючи сам клас:

```csharp
public event AuthHandler? OnAuthenticate;
public event TransactionHandler? OnBalanceCheck;
public event TransactionHandler? OnWithdraw;
public event TransactionHandler? OnDeposit;
public event TransactionHandler? OnTransfer;
```

У [`ConsoleApp/Program.cs`](https://github.com/ipz245imv-droid/lab_1/blob/master/ConsoleApp/Program.cs#L19-L23) виводить у консоль, у [`WinFormsApp/Form1.cs`](https://github.com/ipz245imv-droid/lab_1/blob/master/WinFormsApp/Form1.cs#L24-L28) — через `MessageBox`, але `AutomatedTellerMachine` не змінюється.

---

## 5. Fail Fast

**Опис:** Помилки повинні виявлятися якомога раніше, щоб не поширюватись далі.

**Демонстрація:**

Метод [`Withdraw`](https://github.com/ipz245imv-droid/lab_1/blob/master/BankingLib/Account.cs#L23-L30) у `Account.cs` одразу повертає `false` при невалідних даних:

```csharp
public bool Withdraw(decimal amount)
{
    if (amount <= 0) return false;
    if (amount > Balance) return false;

    Balance -= amount;
    return true;
}
```
