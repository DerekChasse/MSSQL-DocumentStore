using Bogus;

using SqlDocStore.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDocStore.App.Fakers
{
    public class AuthorFaker : Faker<Author>
    {
        public AuthorFaker()
        {
            RuleFor(a => a.Name, f => f.Name.FullName());
            RuleFor(a => a.Email, f => f.Internet.Email());
        }
    }
}
