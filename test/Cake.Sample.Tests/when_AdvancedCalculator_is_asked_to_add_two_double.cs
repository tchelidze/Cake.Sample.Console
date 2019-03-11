using Cake.Sample.Console;
using Machine.Specifications;
using Shouldly;
using System;

namespace Cake.Sample.Tests
{
    public class when_AdvancedCalculator_is_asked_to_add_two_double
    {
        private static AdvancedCalculator _sut;
        private static readonly double left = new Random().Next(100);
        private static readonly double right = new Random().Next(100);
        private static double result;

        private readonly Establish context = () =>
        {
            _sut = new AdvancedCalculator();
        };

        private readonly Because of = () => result = _sut.Add(left, right);

        private readonly It should_return_sum_of_left_and_right = () => result.ShouldBe(left + right);
    }
}
