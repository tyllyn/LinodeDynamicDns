# LinodeDynamicDns
Windows Console App to allow dynamic updates to a DNS entry in Linode.

## Requirements
.NET Framework 4.5.2+

## Setup
Open a command prompt and run the executable with the parameter "setup" to go through an initial setup process.
```
LinodeDynamicDns.exe setup
```
There is no validation, so don't goof around.

This will write the settings to the file `settings.json` for later reference.

## Updating DNS Entries
After the initial setup has been done, simply run the executable to update the DNS to point to the IP of the machine you're running it from.

I would suggest to use Windows Task Scheduler to have this executable run on a schedule.
