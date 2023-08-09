﻿// -----------------------------------------------------------------------
// <copyright file="CreateCustomer.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Store.PartnerCenter.Samples.Customers
{
    using System;
    using System.Globalization;
    using Store.PartnerCenter.Models;
    using Store.PartnerCenter.Models.Customers;

    /// <summary>
    /// Creates a new customer for a partner.
    /// </summary>
    public class CreateCustomer : BasePartnerScenario
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCustomer"/> class.
        /// </summary>
        /// <param name="context">The scenario context.</param>
        public CreateCustomer(IScenarioContext context) : base("Create a new customer", context)
        {
        }

        /// <summary>
        /// Executes the create customer scenario.
        /// </summary>
        protected override void RunScenario()
        {
            var partnerOperations = this.Context.UserPartnerOperations;

            var customerToCreate = new Customer()
            {
                CompanyProfile = new CustomerCompanyProfile()
                {
                    Domain = string.Format(CultureInfo.InvariantCulture, "SampleApplication{0}.{1}", new Random().Next(), this.Context.Configuration.Scenario.CustomerDomainSuffix),
                    //// OrganizationRegistrationNumber = "123456" // Please add if in specific country that requires
                },
                BillingProfile = new CustomerBillingProfile()
                {
                    Culture = "en-US",
                    Email = "gena@relecloud2.com",
                    Language = "en",
                    CompanyName = "Relecloud" + new Random().Next(),
                    DefaultAddress = new Address()
                    {
                        FirstName = "Gena",
                        MiddleName = "MiddleName",
                        LastName = "Soto",
                        AddressLine1 = "4567 Main Street",
                        City = "Redmond",
                        State = "WA",
                        Country = "US",
                        PhoneNumber = "4255550101",
                        PostalCode = "98052"
                    }
                }
            };

            this.Context.ConsoleHelper.WriteObject(customerToCreate, "New customer Information");
            this.Context.ConsoleHelper.StartProgress("Creating customer");

            var newCustomer = partnerOperations.Customers.Create(customerToCreate);

            this.Context.ConsoleHelper.StopProgress();
            this.Context.ConsoleHelper.Success("Success!");
            this.Context.ConsoleHelper.WriteObject(newCustomer, "Created customer Information");
        }
    }
}
