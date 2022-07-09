using NUnit.Framework;
using Selenium.Specflow.Automation.Helpers;
using Selenium.Specflow.Automation.Utilities;
using TechTalk.SpecFlow.Assist;

namespace Selenium.Specflow.Automation.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private readonly DriverHelper _driverHelper;
        private readonly CaptureEvidences _evidences;
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        public CalculatorStepDefinitions(DriverHelper driverHelper)
        {
            _driverHelper = driverHelper;
            _evidences = new CaptureEvidences(_driverHelper.driver);
        }

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            //TODO: implement arrange (precondition) logic
            // For storing and retrieving scenario-specific data see https://go.specflow.org/doc-sharingdata
            // To use the multiline text or the table argument of the scenario,
            // additional string/Table parameters can be defined on the step definition
            // method. 
            Console.WriteLine("First Number ");
            Assert.IsTrue(true);

        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            //TODO: implement arrange (precondition) logic
            Assert.Fail("Intentional Fail");
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            //TODO: implement act (action) logic
            Console.WriteLine("When two numbers are added - intentional Pass ");
            _evidences.CaptureLogs(true, "When Two numbers are added");
            Assert.IsTrue(true);
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            //TODO: implement assert (verification) logic

            throw new PendingStepException();
        }

        [Given(@"the following numbers")]
        public void GivenTheFollowingNumbers(Table table)
        {
            dynamic numbers = table.CreateDynamicSet();
            foreach (var item in numbers)
            {
                Console.WriteLine($"The Number is: {item.Numbers}");
            }
        }


    }
}