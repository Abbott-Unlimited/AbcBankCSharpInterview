# abc-bank-tests Nuget Package Info

### Packages:

<details>
  <summary>
    <b><u>MsTestExtensions</u></b> <sup><sub>(<i>additional extension methods to test for expected errors</i>)</sub></sup>
  </summary>

  #### Links:
  - [MSTestExtensions github repository](https://github.com/bbraithwaite/MSTestExtensions)
  - [Docs (readme.md)](https://github.com/bbraithwaite/MSTestExtensions#readme)
  - [Getting Started](https://github.com/bbraithwaite/MSTestExtensions#get-started)

  #### What problem does this package solve?  
  From the readme docs:
  > I wanted to be able to add my own extension methods to Assert e.g. Assert.Throws() but keeping the existing default MSTest methods.

  Examples
  ```C#     
    [TestClass]
    public class Example_Tests : BaseTest 
    {
      [TestMethod]
      public void Test_Will_Throw_An_Exception() 
      {
        Assert.Throws(() => throw new Exception());
      }

      [TestMethod]
      public void Test_Will_Throw_Known_Exception() 
      {
        Assert.Throws<FileNotFoundException>(() => throw new FileNotFoundException());
      }
    }
  ```

  Async Examples
  ```C#
    [TestClass]
    public class Async_Example_Tests : BaseTest 
    {
      [TestMethod]
      public void Test_Will_Throw_An_Async_Exception() 
      {
        Assert.ThrowsAsync(Task.Run(() => throw new Exception()));
      }

      [TestMethod]
      public void Test_Will_Throw_Async_Known_Exception() 
      {
        Assert.ThrowsAsync<FileNotFoundException>(Task.Run(() => throw new FileNotFoundException()));
      }
    }
  ```
</detail>