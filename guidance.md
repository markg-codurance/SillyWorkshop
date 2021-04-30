### Implementing the api that the bank uses

#### Prerequisite informationals
First thing you should do is build the whole thing:

`dotnet build`

If you haven't already done so, execute the runner on the cmd line so you can see what needs to be implemented next:

`dotnet run --project Runner/`

You will first see it do lots of stuff and print messages like 'unreachable code detected'. To some extent you can ignore those comments, but it is advisable to read the full stack trace so that you get the full clue. So make sure your build output view is large to see it all.

#### The Exercises
Of course this exercise is contrived and there are things that are implemented in a naive manner. That said the concepts should be clear.

In this application you will need to implement IOC dependencies to satisfy the ports, the controller actions so that the bank clerk can carry out some tasks on behalf of an account holder, port implementations that the hexagon can use. You will also have a brief look at an event store to get an appreciation of how they work.

#### The important commentary
There are comments dotted throughout that can give you guidance. I tried to make the exercise like one of those breakout rooms where you need to find the problem have some info and then solve the problem.

You probably want to try individually but have a shared room to talk, I will give you 3 clues to begin with, if it is too hard then I will notice and help more.

#### Outline of Tasks
Things to implement:

 In startup.cs
 - [ ] Registrations so the app knows what is available for use

 In the Controller:
 - [ ] View bank statement
   - [ ] Don't concern yourself with date ranges, just return all
 - [ ] View bank account balance
 - [ ] Deposit to an account
 - [ ] Withdraw from an account
   - [ ] Ensure overdraft is considered

Other:
- [ ] Implement the parts in BalanceReport.cs
- [ ] Implement similar parts in StatementReport.cs
- [ ] Implement behaviour that can discern an overdraft attempt
- [ ] The event store aspect is mostly understanding, read the bit and fix anything that is broken
  - [ ] Implement ReadAll()
  - [ ] Implement Append()
  
 #### What is already done 
 Much of the necessary boilerplatey type functionality will be implemented in other files, perhaps other projects.
 
 #### Sanity check you work
 
When done the Runner should output a value of £53 for the balance.
 
You should be able to run the app if it is truly done:
 
 `dotnet run --project BankDesk`
 
 #### Manual Experimentation
 The controller actions should be obvious, but using these in sequence should give a balance of £77:
 
 http://localhost:5000/bank/deposit/358cc7ca-31ea-4ff7-abc6-c1acd1138d6f/200
 
 http://localhost:5000/bank/withdraw/358cc7ca-31ea-4ff7-abc6-c1acd1138d6f/123
 
 http://localhost:5000/bank/358cc7ca-31ea-4ff7-abc6-c1acd1138d6f
