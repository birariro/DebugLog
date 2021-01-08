# DebugLog
:speech_balloon: C# a chase to catch a bug


### :family: Support
JJinggg(https://github.com/JJinggg)


## DebugLog

### :eyes: Debug Mode
show in your logcat view   


### :ghost: Release Model
not show in your logcat view   

### :warning: Select StackTrace Mode
```C#
public static void e(Exception e)
{
	try
	{
		DetailExceptionLog(e); // select 1
		SimpleExceptionLog(e);// select 2
	}
	catch { };

}
```

### :page_with_curl: instruction
-----------------------------------
```
DebugLog.e(Exception object)
```






### DebugLog DetailExceptionLog Result
-----------------------------------
![디버그로그1](https://user-images.githubusercontent.com/52993842/92225037-a3b5b880-eedd-11ea-9bcf-9a3e1c9c2320.png)


### DebugLog SimpleExceptionLog Result
-----------------------------------
![디버그로그2](https://user-images.githubusercontent.com/52993842/103996765-a6068700-51dd-11eb-8e6a-391e4a877c33.png)


