using System;
using System.Linq;
using FluentAssertions;
using Demo.Rop;
using IntResult = Demo.Rop.Result<int, string>;
using BoolResult = Demo.Rop.Result<bool, string>;
using Xunit;

namespace Demo.Tests
{
	public class ResultTests
	{
		[Fact]
		public void Successes_And_Failures()
		{
			var success = Result<int, bool>.Succeeded(10);

			success.IsSuccess.Should().BeTrue();
			success.IsFailure.Should().BeFalse();

			success.Success.Should().Be(10);

			var failed = Result<int, bool>.Failed(true);
			failed.IsSuccess.Should().BeFalse();
			failed.IsFailure.Should().BeTrue();

			failed.Failure.Should().BeTrue();
		}

		[Fact]
		public void Either_Calls_Expected_Function_On_Success()
		{
			Func<IntResult, BoolResult> onSuccess =
				x => BoolResult.Succeeded(true);
			Func<IntResult, BoolResult> onFailure =
				x => BoolResult.Succeeded(false);

			var successfulInt = Result<int, string>.Succeeded(1);

			var result = successfulInt.Either(onSuccess, onFailure);

			result.ShouldBeEquivalentTo(BoolResult.Succeeded(true));
		}

		[Fact]
		public void Either_Calls_Expected_Function_On_Failure()
		{
			Func<IntResult, BoolResult> onSuccess =
				x => BoolResult.Succeeded(true);
			Func<IntResult, BoolResult> onFailure =
				x => BoolResult.Succeeded(false);

			var failureInt = Result<int, string>.Failed("It's the thought that counts");

			var result = failureInt.Either(onSuccess, onFailure);

			result.ShouldBeEquivalentTo(BoolResult.Succeeded(false));
		}

		[Fact]
		public void ToFailure_Forces_Failure_To_Failure()
		{
			var failure = Result<int, int[]>.Failed(new []{0,1,2});
			var failureToFailure = failure.ToFailure();

			failureToFailure.ShouldBeEquivalentTo(failure);
		}

		[Fact]
		public void ToFailure_Forces_Success_To_Failure()
		{
			var success = Result<int, int[]>.Succeeded(1);
			var successToFailure = success.ToFailure();

			successToFailure.ShouldBeEquivalentTo(Result<int, int[]>.Failed(new int[0]));
		}

		[Fact]
		public void Merge_Puts_Successes_Together_As_A_Success()
		{
			var accumulatedSuccesses = Result<int[], bool[]>.Succeeded(new[] {0, 1});
			var anotherSuccess = Result<int, bool[]>.Succeeded(2);

			var result = accumulatedSuccesses.Merge(anotherSuccess);

			result.ShouldBeEquivalentTo(
				Result<int[], bool[]>.Succeeded(new[] {0, 1, 2})
				);
		}

		[Fact]
		public void Merge_Puts_Success_And_Failure_Together_As_A_Failure()
		{
			var accumulatedSuccesses = Result<int[], bool[]>.Succeeded(new[] {1});
			var aFailure = Result<int, bool[]>.Failed(new[] {true});

			var result = accumulatedSuccesses.Merge(aFailure);

			result.ShouldBeEquivalentTo(
				Result<int[], bool[]>.Failed(new []{true}));
		}

		[Fact]
		public void Merge_Puts_Failure_And_Success_Together_As_A_Failure()
		{
			var accumulatedFailures = Result<int[], bool[]>.Failed(new[] {false});
			var aSuccess = Result<int, bool[]>.Succeeded(1);

			var result = accumulatedFailures.Merge(aSuccess);

			result.ShouldBeEquivalentTo(
				Result<int[], bool[]>.Failed(new []{false}));
		}

		[Fact]
		public void Merge_Puts_Failure_And_Failure_Together_As_A_Failure()
		{
			var accumulatedFailures = Result<int[], bool[]>.Failed(new[] {false, false});
			var aFailure = Result<int, bool[]>.Failed(new[] {true});

			var result = accumulatedFailures.Merge(aFailure);

			result.ShouldBeEquivalentTo(
				Result<int[], bool[]>.Failed(new []{false, false, true}));
		}

		[Fact]
		public void Map_Calls_F_If_Success()
		{
			Func<int, bool> callMe = x => true;

			var aSuccess = Result<int, string[]>.Succeeded(5);

			var result = aSuccess.Map(callMe);
			result.ShouldBeEquivalentTo(Result<bool,string[]>.Succeeded(true));
		}

		[Fact]
		public void Map_Passes_Through_Failures()
		{
			Func<int, bool> callMe = x => true;

			var aFailure = Result<int, string[]>.Failed(new []{"1", "2"});

			var result = aFailure.Map(callMe);
			result.ShouldBeEquivalentTo(Result<bool,string[]>.Failed(new []{"1", "2"}));
		}

		[Fact]
		public void Bind_Is_Awesome()
		{
			var aSuccess = Result<int, string>.Succeeded(1);
			Func<int, Result<bool, string>> funkyTown = 
				i => Result<bool, string>.Succeeded(true);

			BoolResult result = aSuccess.Bind(funkyTown);

			result.ShouldBeEquivalentTo(Result<bool, string>.Succeeded(true));

			var aFailure = Result<int, string>.Failed("Failure is always an option (monad), although choice monad might be better");

			BoolResult result2 = aFailure.Bind(funkyTown);

			result2.ShouldBeEquivalentTo(
				Result<bool, string>.Failed("Failure is always an option (monad), although choice monad might be better")
				);
		}

        [Fact]
	    public void Aggregate_Array_Empty()
        {
            var result = new Result<int, string[]>[0].Aggregate();
            result.IsSuccess.Should().BeTrue();
            result.Success.Should().BeEmpty();
        }

	    [Fact]
	    public void Aggregate_Array_Successes()
	    {
	        var result = new []
	        {
	            Result<int, string[]>.Succeeded(1),
	            Result<int, string[]>.Succeeded(2),
	            Result<int, string[]>.Succeeded(3)
	        }.Aggregate();

	        result.IsSuccess.Should().BeTrue();
	        result.Success.ShouldBeEquivalentTo(new [] {1,2,3});
        }

	    [Fact]
	    public void Aggregate_Array_Failure()
	    {
	        var result = new[]
	        {
	            Result<int, string[]>.Succeeded(1),
	            Result<int, string[]>.Failed(new [] {"Wrong"}),
	            Result<int, string[]>.Succeeded(2),
	            Result<int, string[]>.Failed(new [] {"Still Wrong", "Wronger"}),
	            Result<int, string[]>.Succeeded(3)

	        }.Aggregate();

	        result.IsFailure.Should().BeTrue();
	        result.Failure.ShouldBeEquivalentTo(new[] { "Wrong", "Still Wrong", "Wronger"});
        }
    }
}