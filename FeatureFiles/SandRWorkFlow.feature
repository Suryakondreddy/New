Feature: SandRWorkFlow

A short summary of the feature

@tag1
Scenario Outline: 01Test Case 1537268: Verify that battery ADL data is restored on original device

	Given [Change channel side in FDTS<DeviceLeft>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"
	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
    When [Verify StorageLayout Scenario By Changing Date and Confirm Cloud Icon "<DeviceId>" and "<DeviceLeft>" and "<DeviceLeftSlNo>"]
	When [Cleaning up Capture and Restore Reports Before Launch SandR]
	When [Change communication channel in S and R<DeviceLeft>]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"]
	When [Go to logs and verify capturing time]
	When [Launch algo and alter ADL value "<DeviceId>" and "<DeviceLeftSlNo>"]
	When [Perform Restore with above captured image "<DeviceId>" and "<DeviceLeftSlNo>"]
	When [Launch algo lab and check the ADL value "<DeviceId>" and "<DeviceLeftSlNo>"]
	When [Go to log file for verifying Restore time] 
	And  [Open Capture and Restore report and log info in report]
	Then [done]

	Examples:

	| DeviceId   | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft |
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |
	#| RE962-DRW |   2049043374   | Yes     | Right       | Left       |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left       |
	 | RT962-DRW | 2000800269     | Yes     | Right       | Left       |
	#| LT988-DW | 1600804970     | Yes     | Right       | Left       |
	#| RT961-DRWC | 2000816934     | Yes     | Right       | Left       | 

	



Scenario Outline: 02Test Case 1103972: Verify device information is shown correctly

	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly]
	When [Come back to Settings and wait till controls enabled]
	Then [Close SandR tool]
	


	Examples:
	| DeviceId   | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | 
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |
	#| RE962-DRW | 2049043374     | Yes     | Right       | Left       |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left       |
	 | RT962-DRW | 2000800269     | Yes     | Right       | Left       |
	#| LT988-DW | 1600804970     | Yes     | Right       | Left       |
	#| RT961-DRWC | 2000816934     | Yes     | Right       | Left       | 




Scenario Outline: 03Test Case 1105474: Verify capture operation is performed within desired time

	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"]
	When [Go to logs and verify capturing time]

	Examples:
	| DeviceId  | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft |
     #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |
	 #| RE962-DRW | 2049043374     | Yes     | Right       | Left       |
	 #| RE962-DRWT | 2000803069     | Yes     | Right       | Left       |
	 | RT962-DRW | 2000800269     | Yes     | Right       | Left       |
	 #| LT988-DW | 1600804970     | Yes     | Right       | Left       |
	 #| RT961-DRWC | 2000816934     | Yes     | Right       | Left       | 

Scenario Outline: 04Test Case 1103482: Verify supported PC configuration

	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"]
	When [Perform Restore with above captured image "<DeviceId>" and "<DeviceLeftSlNo>"]

	Examples:

	| DeviceId   | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | 
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |
	#| RE962-DRW | 2049043374     | Yes     | Right       | Left       |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left       |
	 | RT962-DRW | 2000800269     | Yes     | Right       | Left       |
	#| LT988-DW | 1600804970     | Yes     | Right       | Left       |
	#| RT961-DRWC | 2000816934     | Yes     | Right       | Left       | 



Scenario Outline: 05Test Case 1103833: Verify channel can be changed while S&R tool is running

	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
	When [Change communication channel in S and R<DeviceLeft>]

	Examples:
	| DeviceId   | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | 
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |
	#| RE962-DRW | 2049043374     | Yes     | Right       | Left       |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left       |
	 | RT962-DRW | 2000800269     | Yes     | Right       | Left       |
	#| LT988-DW | 1600804970     | Yes     | Right       | Left       |
	#| RT961-DRWC | 2000816934     | Yes     | Right       | Left       |




Scenario Outline: 06Test Case 1104002: Verify HI capture/restoration report

	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
	When [Cleaning up Capture and Restore Reports Before Launch SandR]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"]
	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
	When [Perform Restore with above captured image "<DeviceId>" and "<DeviceLeftSlNo>"]
	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
	And  [Open Capture and Restore report and log info in report]

	Examples:
	| DeviceId   | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | 
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |
	#| RE962-DRW | 2049043374     | Yes     | Right       | Left       |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left       |
	 | RT962-DRW | 2000800269     | Yes     | Right       | Left       |
	#| LT988-DW | 1600804970     | Yes     | Right       | Left       |
	#| RT961-DRWC | 2000816934     | Yes     | Right       | Left       |

	
Scenario Outline: 07Test Case 1142328: PC_Verify HI can be PC programmed properly.

	 When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
	 Given [Change channel side in FDTS<DeviceLeft>]
	 Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"
	 When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceSlNo>" and "<DeviceRight>"]
	 Given [Change channel side in FDTS<DeviceRight>]
	 Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"

	Examples:
	| DeviceId  | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | DeviceSlNo |
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |
	#| RE962-DRW | 2049043374      | Yes     | Right       | Left       | 2026484509 |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left       | 2000803066 |
     | RT962-DRW  | 2000800269     | Yes     | Right       | Left       | 2000800246 |
	#| LT988-DW   | 1600804970    | Yes     | Right       | Left       | 1600804918 |
	#| RT961-DRWC | 2000816934     | Yes     | Right       | Left       | 2000816936 |




Scenario Outline: 08Test Case 1103981: Verify device information is cleared when HI is disconnected

		When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
		When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly]
		When [Come back to Settings and wait till controls enabled]
		When [Clicks on disconnect and verify device information is cleared]

		Examples:b


	| DeviceId   | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | 
    #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |
	#| RE962-DRW | 2026335111     | Yes     | Right       | Left       |
	#| RE962-DRWT | 2000803069     | Yes     | Right       | Left       |
	#| RT962-DRW | 2000800247     | Yes     | Right       | Left       |
	| LT988-DW | 1600806099     | Yes     | Right       | Left       |
	#| RT961-DRWC | 2000801965     | Yes     | Right       | Left       | 



Scenario Outline: 09Test Case 1105498: Verify that S&R Tool properly sets listening test settings
		        
				When [Create a Patient and add programs to HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
				Given [Change channel side in FDTS<DeviceLeft>]
    			Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"
				Given [Change channel side in FDTS<DeviceRight>]    
				Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"
				When [Create a Patient and add programs to HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
				When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
				When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly]
				When [Come back to Settings and wait till controls enabled]
				When [Perform Capture with listening test settings]
				Then [Launch FSW and check the added programs "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]


	Examples:
	| DeviceId   | DeviceLeftSlNo | FlashHI | DeviceRight | DeviceLeft | DeviceSlNo | 
     #| LT961-DRW-UP | 2000800436   | Yes     | Right       | Left       |1700800900|
	 #| RE962-DRWT | 2000803069     | Yes     | Right       | Left       | 2000803066 |
	 #| RE962-DRW |    2049043374  | Yes     | Right       | Left       | 2026484509 |
	 | RT962-DRW | 2000800269     | Yes     | Right       | Left       | 2000800246 |
	 #| LT988-DW | 1600804970     | Yes     | Right       | Left       | 1600804918 |
	 #| RT961-DRWC  | 2000816934     | Yes     | Right       | Left       | 2000816936 | 





Scenario Outline: 10Test Case 1105696: Verify that fitting data is properly restored during restoration on new device (RTS)
	Given [Cleaning up dumps before execution starts]
	Given [Change channel side in FDTS<DeviceLeft>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"
	Given [Change channel side in FDTS<DeviceRight>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"
	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
	When [Cleaning up Capture and Restore Reports Before Launch SandR]
	When [Change communication channel in S and R<DeviceLeft>]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
	When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"]
	When [Go to logs and verify capturing time]
	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
    When [Get the dump of connected device by storage layout "<DeviceId>" and "<DeviceLeft>" and "<DeviceLeftSlNo>"] 
	And  [Open Capture and Restore report and log info in report]
	Given [Change channel side in FDTS<DeviceRight>]
	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"
	When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceSlNo>" and "<DeviceRight>"]
 	When [Perform Restore with above captured image using RTS option "<DeviceLeftSlNo>" and "<DeviceSlNo>" and "<DeviceId>" and "<DeviceRight>"]
	When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceSlNo>" and "<DeviceRight>"]
    When [Get the dump of connected device by storage layout "<DeviceId>" and "<DeviceRight>" and "<DeviceSlNo>"]
	Then [Do the dump comparison between two device dumps<DumpB>]
	When [Change communication channel in S and R<DeviceLeft>]
	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
	When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
	When [Come back to Settings and wait till controls enabled]
	When [Perform Capture"<DeviceId>"]
	When [Go to logs and verify capturing time]
	When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
	When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceSlNo>" and "<DeviceRight>"]
    When [Get the dump of connected device by storage layout "<DeviceId>" and "<DeviceTemp>" and "<DeviceSlNo>"]
	Then [Do the dump comparison between two device dumps<DumpC>]
	When [Perform Restore with above captured image using RTS option "<DeviceLeftSlNo>" and "<DeviceSlNo>" and "<DeviceId>" and "<DeviceRight>"]
	When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceSlNo>" and "<DeviceRight>"]
    When [Get the dump of connected device by storage layout "<DeviceId>" and "<DeviceTemp>" and "<DeviceSlNo>"]
	Then [Do the dump comparison between two device dumps<DumpC>]

	Examples:
	| DeviceId     | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DeviceTemp | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo   | FlashHI | 

	 #| RE962-DRW   | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 2026484509 | NoDev    | 2049043374       | Yes     |
	 #| RE962-DRWT   | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 2000803066 | NoDev    | 2000803069       | Yes     |
	 #| LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 1700800900 | NoDev    | 2000800436       | Yes     |
	 # | LT988-DW | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 1600804918 | NoDev    | 1600804970       | Yes     |
	 #| RE961-DRWC | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 2156716945 | NoDev    | 2156716944       | Yes     |
	 | RT962-DRW | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 2000800246 | NoDev    | 2000800269       | Yes     |
	 #| RT961-DRWC | Left       | Right       | Device A | Device B | Device C | Temp       | Yes      | No         | 2000816936 | NoDev    | 2000816934       | Yes     |

	

	Scenario Outline: 11Test Case 1105669: Verify that fitting data is properly restored during restoration on original device or Clone (SWAP)
        
		
		Given [Cleaning up dumps before execution starts]
        Given [Change channel side in FDTS<DeviceLeft>]
	 	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"
		Given [Change channel side in FDTS<DeviceRight>]    
		Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"
		When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
		When [Get the dump of connected device by storage layout "<DeviceId>" and "<DeviceLeft>" and "<DeviceLeftSlNo>"] 
        When [Cleaning up Capture and Restore Reports Before Launch SandR]
        When [Change communication channel in S and R<DeviceLeft>]
		When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
		When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
        When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly]
        When [Come back to Settings and wait till controls enabled]
        When [Perform Capture"<DeviceId>"]
		When [Go to logs and verify capturing time]
		Given [Change channel side in FDTS<DeviceLeft>]
    	Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"
		When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
		When [Get the dump of connected device left of DumpB by storage layout "<DeviceId>" and "<DeviceLeft>" and "<DeviceLeftSlNo>"]
        Then [Do the dump comparison between two device dumps<DumpB>]
		When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
        When [Perform Restore with above captured image using SWAP option "<DeviceSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceLeft>"]
		When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
        When [Get the dump of connected device of left DumpC by storage layout "<DeviceId>" and "<DeviceLeft>" and "<DeviceLeftSlNo>"]
        Then [Do the dump comparison between two device dumps<DumpC>]
        When [Change communication channel in S and R<DeviceLeft>]
		When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
        When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
        When [Go to Device Info tab and capture device info in excel then verify the device information is shown correctly]
        When [Come back to Settings and wait till controls enabled]
        When [Perform Capture"<DeviceId>"]
        When [Go to logs and verify capturing time]
		When [Create a Patient and Fitting HI In FSW "<AlterFSWNo>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
		Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"
		Given [Change channel side in FDTS<DeviceRight>] 
        Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceSlNo>" and "<FlashHI>" and "<DeviceRight>"
        When [Perform Restore with above captured image using SWAP with left "<DeviceSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceRight>"]
        When [Get the dump of connected device of DumpD by storage layout "<DeviceId>" and "<DeviceRight>" and "<DeviceLeftSlNo>"] 
		#This above step modified from leftsl no to right sl no
        Then [Do the dump comparison between two device DeviceC and DeviceD dumps<DumpD>]

     Examples:
    | DeviceId  | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | DeviceTemp | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI |

    #| RE962-DRW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2026484509  | NoDev        | 2049043374     | Yes     |
	#| RE962-DRWT   | Left       | Right       | Device A| Device B | Device C | Device D | Temp       | Yes      | No         | 2000803066 | NoDev    | 2000803069     | Yes     |
	#| LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1700800900 | NoDev    | 2000800436     | Yes     |
	#| LT988-DW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1600804918 | NoDev    | 1600804970     | Yes     |
	#| RE961-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2156716945 | NoDev    | 2156716944       | Yes     |
	 | RT962-DRW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2000800269 | NoDev    | 2000800246       | Yes     |
	#| RT961-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2000816936 | NoDev    | 2000816934     | Yes     |




Scenario Outline: 12Test case 1629628: Verify that firmware is upgraded if conditions apply

			Given [Change channel side in FDTS<DeviceLeft>]
			Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"
			When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
			When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
			When [Come back to Settings and wait till controls enabled]
			When [Perform Capture"<DeviceId>"]
			When [Perform Restore with above captured image using SWAP option "<DeviceSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceLeft>"]
			When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
			Then [Compare firmware version is upgraded successfully "<DeviceId>"]




			Examples:
    | DeviceId  | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | DeviceTemp | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI |

    | RE962-DRW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2026484509  | NoDev        | 2049043374     | Yes     |
	#| RE962-DRWT   | Left       | Right       | Device A| Device B | Device C | Device D | Temp       | Yes      | No         | 2000803066 | NoDev    | 2000803069     | Yes     |
	#| LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1700800900 | NoDev    | 2000800436     | Yes     |
	#| LT988-DW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1600804918 | NoDev    | 1600804970     | Yes     |
	#| RE961-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2156716945 | NoDev    | 2156716944       | Yes     |
	#| RT962-DRW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2000800269 | NoDev    | 2000800246       | Yes     |
	#| RT961-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2000816934 | NoDev    | 2000816936     | Yes     |


Scenario Outline: 13Test Case 1629629: Verify that firmware is downgraded if conditions apply


			Given [Change channel side in FDTS<DeviceLeft>]
			Given Launch FDTS WorkFlow And Flash Device "<DeviceId>" and "<DeviceLeftSlNo>" and "<FlashHI>" and "<DeviceLeft>"
			When [Create a Patient and Fitting HI In FSW "<AlterFSW>" and "<DeviceId>" and "<DeviceLeftSlNo>" and "<DeviceLeft>"]
			When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
			When [Come back to Settings and wait till controls enabled]
			When [Perform Capture"<DeviceId>"]
			When [Perform Restore with above captured image using SWAP option "<DeviceSlNo>" and "<DeviceLeftSlNo>" and "<DeviceId>" and "<DeviceLeft>"]
			When [Launch SandR "<DeviceId>" and "<DeviceLeftSlNo>"]
			Then [Compare firmware version is downgraded successfully "<DeviceId>"]


			
			Examples:
    | DeviceId  | DeviceLeft | DeviceRight | DumpA    | DumpB    | DumpC    | DumpD    | DeviceTemp | AlterFSW | AlterFSWNo | DeviceSlNo | NoDevice | DeviceLeftSlNo | FlashHI |

    | RE962-DRW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2026484509  | NoDev        | 2049043374     | Yes     |
	#| RE962-DRWT   | Left       | Right       | Device A| Device B | Device C | Device D | Temp       | Yes      | No         | 2000803066 | NoDev    | 2000803069     | Yes     |
	#| LT961-DRW-UP | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1700800900 | NoDev    | 2000800436     | Yes     |
	#| LT988-DW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 1600804918 | NoDev    | 1600804970     | Yes     |
	#| RE961-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2156716945 | NoDev    | 2156716944       | Yes     |
	#| RT962-DRW | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2000800269 | NoDev    | 2000800246       | Yes     |
	#| RT961-DRWC | Left       | Right       | Device A | Device B | Device C | Device D | Temp       | Yes      | No         | 2000816934 | NoDev    | 2000816936     | Yes     |



	


