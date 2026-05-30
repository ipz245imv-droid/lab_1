# Programming Principles

## 1. DRY (Don't Repeat Yourself)

Принцип DRY дотримується у класі `AutomatedTellerMachine` — перевірка `amount <= 0` винесена окремо на початку кожного методу, а не дублюється всередині логіки. Також метод `SetUiLoggedIn` у `Form1.cs` уникає повторення коду вмикання/вимикання кнопок.

**Приклад:**
- [`AutomatedTellerMachine.cs`, методи `Deposit`, `Withdraw`, `Transfer`](AutomatedTellerMachine.cs) — однотипна валідація суми на початку кожного методу.
- [`Form1.cs`, метод `SetUiLoggedIn`](Form1.cs#L36-L44) — централізоване керування станом UI.

---

## 2. SRP (Single Responsibility Principle)

Кожен клас має одну зону відповідальності:

- `Account` — зберігає дані рахунку та виконує операції з балансом.
- `Bank` — управляє колекцією рахунків.
- `AutomatedTellerMachine` — реалізує логіку банкомату (автентифікація, операції).
- `Form1` — відповідає лише за UI та взаємодію з користувачем.

**Приклад:**
- [`Account.cs`](Account.cs) — містить лише дані та методи `Deposit`/`Withdraw`.
- [`Bank.cs`](Bank.cs) — містить лише список рахунків та метод пошуку `FindAccount`.

---

## 3. OCP (Open/Closed Principle)

Клас `AutomatedTellerMachine` відкритий для розширення через події (delegates/events), але закритий для модифікації. Нову поведінку при автентифікації чи транзакції можна додати підпискою на відповідну подію, не змінюючи сам клас.

**Приклад:**
- [`AutomatedTellerMachine.cs`, рядки з `event`](AutomatedTellerMachine.cs#L13-L18) — події `OnAuthenticate`, `OnDeposit`, `OnWithdraw`, `OnTransfer`, `OnBalanceCheck`.
- [`Form1.cs`](Form1.cs#L27-L32) та [`ConsoleApp/Program.cs`](Program.cs#L19-L23) — різна поведінка (MessageBox vs Console) підключається зовні без зміни ATM.

---

## 4. KISS (Keep It Simple, Stupid)

Код написаний просто і без зайвої складності. Методи короткі та зрозумілі. `Bank.FindAccount` реалізований в один рядок за допомогою LINQ.

**Приклад:**
- [`Bank.cs`, метод `FindAccount`](Bank.cs#L11-L12) — лаконічна реалізація через `FirstOrDefault`.
- [`Account.cs`, методи `Deposit`/`Withdraw`](Account.cs#L19-L30) — проста і зрозуміла логіка без зайвих абстракцій.

---

## 5. Fail Fast

Методи одразу повертають результат або виходять при невалідних даних, не продовжуючи виконання.

**Приклад:**
- [`Account.cs`, метод `Withdraw`](Account.cs#L23-L27) — перевірки `amount <= 0` та `amount > Balance` на початку методу.
- [`AutomatedTellerMachine.cs`, метод `Transfer`](AutomatedTellerMachine.cs#L72-L95) — послідовні перевірки суми, наявності отримувача та балансу перед виконанням переказу.
