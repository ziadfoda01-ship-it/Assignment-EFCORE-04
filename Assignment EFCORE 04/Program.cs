using Assignment_EFCORE_04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using static Assignment_EFCORE_04.Models.Account;
using static Assignment_EFCORE_04.Models.Customer;
using static Assignment_EFCORE_04.Models.customerAccount;

namespace Assignment_EFCORE_04
{
    internal class Program
    {
        static void Main(string[] args)
        {

            SeedDatabase();
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("============================");
                Console.WriteLine("   National Bank Management ");
                Console.WriteLine("============================");
                Console.WriteLine("1) Add a new Customer");
                Console.WriteLine("2) Open a new Account for a Customer");
                Console.WriteLine("3) Update Account Status (Active / Closed)");
                Console.WriteLine("4) Remove an Account from a Customer");
                Console.WriteLine("5) List all Customers (with accounts)");
                Console.WriteLine("0) Exit");
                Console.Write("Enter choice: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        AddNewCustomer();
                        break;
                    case "2":
                        OpenNewAccount();
                        break;
                    case "3":
                        UpdateAccountStatus();
                        break;
                    case "4":
                        RemoveAccountFromCustomer();
                        break;
                    case "5":
                        ListAllCustomers();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Exiting application. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please enter a number between 0 and 5.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
            }
        }

        // ميثود إدخال البيانات الأولية
        private static void SeedDatabase()
        {
            using var context = new AbbDbContext();
            context.Database.EnsureCreated();

            if (!context.Branches.Any())
            {
                var manager1 = new Manager
                {
                    FullName = "Mahmoud Ali",
                    Email = "m.ali@bank.com",
                    PhoneNumber = "01111111111",
                    HireDate = DateTime.Now,
                    Address = "Giza"
                };
                context.Managers.Add(manager1);
                context.SaveChanges();

                var branch = new Branch
                {
                    Code = "CAI-01",
                    Name = "Cairo Main Branch",
                    Address = "Downtown, Cairo",
                    PhoneNumber = "022222222",
                    ManagerId = manager1.Id
                };

                context.Branches.Add(branch);
                context.SaveChanges();
            }
        }

        // 1) إضافة عميل جديد
        private static void AddNewCustomer()
        {
            try
            {
                Console.WriteLine("--- Add New Customer ---");
                Console.Write("Full Name: ");
                string name = Console.ReadLine();

                Console.Write("National ID: ");
                string natId = Console.ReadLine();

                Console.Write("Date of Birth (yyyy-MM-dd): ");
                DateTime dob = DateTime.Parse(Console.ReadLine());

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Phone: ");
                string phone = Console.ReadLine();

                Console.Write("Address: ");
                string address = Console.ReadLine();

                Console.WriteLine("Customer Type:\n1) Individual\n2) Business");
                Console.Write("Choice: ");
                int typeChoice = int.Parse(Console.ReadLine());
                CustomerType cType = typeChoice == 2 ? CustomerType.Busienss : CustomerType.Individual;

                using var context = new AbbDbContext();
                var customer = new Customer
                {
                    FullName = name,
                    NationalId = natId,
                    DateOfBirth = dob,
                    Email = email,
                    PhoneNumber = phone,
                    Address = address,
                    customerType = cType
                };

                context.Customers.Add(customer);
                context.SaveChanges();

                Console.WriteLine($"\nCustomer created successfully. CustomerId = {customer.Id}");
            }
            catch (Exception ex)
            {
                Console. WriteLine($"\nError: Invalid input format. Details: {ex.Message}");
            }
        }

        // 2) فتح حساب جديد
        private static void OpenNewAccount()
        {
            try
            {
                Console.WriteLine("--- Open New Account ---");
                Console.Write("Account Number: ");
                string accNo = Console.ReadLine();

                Console.WriteLine("Account Type:\n1) Savings\n2) Current\n3) Business");
                Console.Write("Choice: ");
                int typeChoice = int.Parse(Console.ReadLine());
                AccountType accType = typeChoice switch { 2 => AccountType.Current, 3 => AccountType.Business, _ => AccountType.Saving };

                Console.Write("Branch Code: ");
                string branchCode = Console.ReadLine();

                Console.Write("Customer Id: ");
                int customerId = int.Parse(Console.ReadLine());

                Console.WriteLine("Ownership Role:\n1) Primary\n2) CoHolder");
                Console.Write("Choice: ");
                int roleChoice = int.Parse(Console.ReadLine());
                OwnerShipRole role = roleChoice == 2 ? OwnerShipRole.CoHolder : OwnerShipRole.Primary;

                using var context = new AbbDbContext();

                var branch = context.Branches.Find(branchCode);
                var customer = context.Customers.Find(customerId);

                if (branch == null || customer == null)
                {
                    Console.WriteLine("\nError: Branch or Customer does not exist!");
                    return;
                }

                Console.WriteLine($"Validating branch '{branchCode}' and customer #{customerId}...");

                var account = context.Accounts.Find(accNo);
                if (account == null)
                {
                    account = new Account
                    {
                        AccountNumber =accNo ,
                        accountType = accType,
                        BranchCode = branchCode,
                        CurrentBalance = 0,
                        OpeningDate = DateTime.Now,
                        accountStatus = AccountStatus.Active
                    };
                    context.Accounts.Add(account);
                }

                var customerAccount = new customerAccount
                {
                    CustomerId = customerId,
                    AccountNumber = accNo,
                    ownerShip = role,
                    dateTime = DateTime.Now
                };

                context.CustomerAccounts.Add(customerAccount);
                context.SaveChanges();

                Console.WriteLine($"\nAccount {accNo} created and linked to customer {customerId} as {role}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }

        // 3) تحديث حالة الحساب
        private static void UpdateAccountStatus()
        {
            try
            {
                Console.WriteLine("--- Update Account Status ---");
                Console.Write("Account Number: ");
                string accNo = Console.ReadLine();

                Console.WriteLine("New Status:\n1) Active\n2) Closed");
                Console.Write("Choice: ");
                int statusChoice = int.Parse(Console.ReadLine());
                AccountStatus newStatus = statusChoice == 2 ? AccountStatus.Closed : AccountStatus.Active;

                using var context = new AbbDbContext();
                var account = context.Accounts.Find(accNo);

                if (account == null)
                {
                    Console.WriteLine("\nAccount not found!");
                    return;
                }

                account.accountStatus = newStatus;
                context.SaveChanges();

                Console.WriteLine($"\nStatus updated to {newStatus}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }

        // 4) حذف حساب من العميل
        private static void RemoveAccountFromCustomer()
        {
            try
            {
                Console.WriteLine("--- Remove Account From Customer ---");
                Console.Write("Account Number: ");
                string accNo = Console.ReadLine();

                Console.Write("Customer Id: ");
                int customerId = int.Parse(Console.ReadLine());

                using var context = new AbbDbContext();
                var link = context.CustomerAccounts
                    .FirstOrDefault(ca => ca.AccountNumber == accNo && ca.CustomerId == customerId);

                if (link == null)
                {
                    Console.WriteLine("\nOwnership link not found!");
                    return;
                }

                context.CustomerAccounts.Remove(link);
                context.SaveChanges();
                
                
                Console.WriteLine("Ownership link deleted.");

                int remainingOwners = context.CustomerAccounts.Count(ca => ca.AccountNumber == accNo);
                if (remainingOwners == 0)
                {
                    var account = context.Accounts.Find(accNo);
                    if (account != null)
                    {
                        context.Accounts.Remove(account);
                        context.SaveChanges();
                        Console.WriteLine($"That was the last owner, account '{accNo}' was also removed.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }

        // 5) عرض كل العملاء بالحسابات (Eager Loading)
        private static void ListAllCustomers()
        {
            using var context = new AbbDbContext();
            var customers = context.Customers
                .Include(c => c.customerAccounts)
                    .ThenInclude(ca => ca.Account)
                        .ThenInclude(a => a.branch)
                .ToList();

            Console.WriteLine("--- All Customers ---");
            foreach (var c in customers)
            {
                Console.WriteLine($"#{c.Id} {c.FullName} ({c.customerType})");

                if (c.customerAccounts == null || !c.customerAccounts.Any())
                {
                    Console.WriteLine("  (no accounts)");
                }
                else
                {
                    foreach (var ca in c.customerAccounts)
                    {
                        var acc = ca.Account;
                        Console.WriteLine($"  {acc.AccountNumber} {acc.accountType} Current Balance: {acc.CurrentBalance:N2} {ca.ownerShip} {acc.accountStatus} @ {acc.branch?.Name}");
                    }
                }

            }
        }
       
        
        }
    }


