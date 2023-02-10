using System;

namespace abc_bank {

// todo:  Ask about what the purpose of this class is.
//        eg:   Why a singleton to wrap DateTime.Now - I am not aware
//              of anything that would prompt creation of this - nor any need...
//        May learn something :)
  public class DateProvider {
    private static DateProvider instance = null;

    public static DateProvider getInstance() {
      return instance == null
        ? new DateProvider()
        : instance;
    }

    public DateTime Now() {
      return DateTime.Now;
    }
  }
}
