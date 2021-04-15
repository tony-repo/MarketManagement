using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Xunit;

namespace Tests.Controllers
{
    public abstract class BaseTests
    {
        private Fixture _fixture;
        public Fixture Fixture
        {
            get
            {
                if (_fixture == null)
                {
                    _fixture = new Fixture();
                }

                return _fixture;
            }
        }
    }
}
