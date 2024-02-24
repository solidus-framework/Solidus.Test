# Solidus.Test

[![Build and Test](https://github.com/solidus-framework/Solidus.Test/actions/workflows/build_and_test.yml/badge.svg)](https://github.com/solidus-framework/Solidus.Test/actions/workflows/build_and_test.yml)

The test base package that covers common test project boilerplate.

## Setup test project
* Reference NuGet package

    ```sh
    dotnet add package Solidus.Test
    ```

* Add global usings for convenience

    ```sh
    global using Solidus.Test;
    ```

## Environment Variables usage example

`.\.github\workflows\dotnet.yml`

```yml
...
    - name: Test
      env:
        TEST_HOUSE_NAME: ${{ secrets.TEST_HOUSE_NAME }}
        TEST_HOUSE_CONFIGURATION: ${{ vars.TEST_HOUSE_CONFIGURATION }}
      run: dotnet test --no-build --verbosity normal
...
```

`.\UnitTests\HouseTest.cs`

```csharp
public class HouseTest : TestBase
{
    [Test]
    public void HappyPath()
    {
        // Arrange.
        var houseConfiguration = env.ReadJsonFromEnvironmentVariable<HouseConfiguration>("TEST_HOUSE_CONFIGURATION");
        var sut = new House();
        sut.Name = env.ReadEnvironmentVariable("TEST_HOUSE_NAME");
        sut.RoomCount = houseConfiguration.RoomCount;

        // Act/Assert.
        ...
    }
}
```

## Mother Factory usage example

`.\MotherFactory\PersonMotherFactory.cs`

```csharp
public static class PersonMotherFactory
{
    public static object CreatePerson(this MotherFactory mf, ISpecialty specialty = null, IEmployment employment = null)
    {
        specialty ??= Mock.Of<ISpecialty>();
        employment ??= Mock.Of<IEmployment>();
        return new(
            specialty: specialty,
            employment: employment);
    }
}
```

`.\UnitTests\HouseEnterTests.cs`

```csharp
public class HouseEnterTests : TestBase
{
    [Test]
    public void PersonEntersHouse_HousePersonsContainPerson()
    {
        // Arrange.
        var sut = new House();
        var person = mf.CreatePerson();

        // Act.
        sut.Enter(person);

        // Assert.
        sut.Person.Should().Contain(person);
    }
}
```

## Test Data Generator usage example

By default Test Data Generator will use AutoFixture to create test data value.

AutoFixture it's a third-party project. Repository located at: https://github.com/AutoFixture/AutoFixture

Simple code snippet that demonstrates a basic string data initialization, that avoids use of magic strings and abstracts test from unnecessary explicit data specification:

```c#
public class PersonTest : TestBase
{
    [Test]
    public void SetName_NameChanged()
    {
        // Arrange.
        var sut = new Person();
        sut.Name = gen.Create<string>();

        var expected = gen.Create<string>();

        // Act.
        sut.Name = expected;

        // Assert.
        sut.Name.Should().Be(expected);
    }
}
```

To change default Test Data Generator over all tests call

```csharp
    DefaultTestDataGenerator.SetDefaultTestDataGenerator(() => ...);
```

To change Test Data Generator for specific test class

```csharp
public class SomeTest : TestBase
{
    protected override ITestDataGenerator CreateTestDataGenerator()
    {
        return ...
    }
}
```
