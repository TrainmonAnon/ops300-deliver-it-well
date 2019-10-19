using System;
using Xunit;
using ops300_deliver_it_well;
using ops300_deliver_it_well.Pages;

namespace ops300_deliver_it_wellTests
{
    public class UnitTest1
    {
        [Fact]
        public void AboutMessageTest()
        {
            string expectedMessage = "Your application description page.";
            AboutModel about = new AboutModel();

            about.OnGet();
            string aboutMessage = about.Message;

            Assert.Equal(expectedMessage, aboutMessage);
        }

        [Fact]
        public void ContactMessageTest()
        {
            string expectedMessage = "Your contact page.";
            ContactModel contact = new ContactModel();

            contact.OnGet();
            string contactMessage = contact.Message;

            Assert.Equal(expectedMessage, contactMessage);
        }
    }
}
