//
//  Crash.h
//  crash iOS
//
//  Created by Mitchell Duncan on 15/04/19.
//

#ifndef Crash_h
#define Crash_h

/**
 Generates native errors for testing Raygun's Crash Reporting.
 
 ```objc
 import "Crash.h"
 ```
 */
@interface Crash : NSObject

/**
 Generates a NSException exception and raises it.
 
 ```objc
 import "Crash.h"
 ...
 
 [Crash throwGenericException];
 ```
 */
+ (void)throwGenericException;

@end


#endif /* Crash_h */
