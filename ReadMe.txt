04 Nov 2022
-----------

Pre-requisite: 
	(1) JRE
	I have an older version of JRE already downloaded and it works for me.
	You have to download and unzip at some location - No MSI or installation, but setting up of some environment vars
		CLASSPATH=C:\Prasun\Software\Java Server JRE\jdk1.8.0_202\lib
		PATH (add to) C:\Prasun\Software\Java Server JRE\jdk1.8.0_202\bin

	(2) Cassandra
	I have an older version 3.0.9 that used to come as an MSI. Requires JRE and some env vars
		CASSANDRA_HOME=C:\Program Files\DataStax Community\apache-cassandra\
		PATH (add to) C:\Program Files\DataStax Community\apache-cassandra\bin

	(3) Pithos Server
	Download Pithos server from somewhere and extract to some folder. I already have pithos-0.7.5-standalone.jar. You need some tweaking in pithos.yaml
	Pithos server requires JRE to run, and uses Cassandra to simulate S3

	ref: http://pithos.io/quickstart.html

	One Time Setup
		java -jar pithos-0.7.5-standalone.jar -f pithos.yaml -a install-schema
	Onwards, start pithos server with this command
		java -jar pithos-0.7.5-standalone.jar -f pithos.yaml

About
	Pithos Provides you AWS S3 interface, but behind the scenes stores data into local Cassandra
	This can be useful if you write an application to talk to S3, but want an on-prem deployment of the same, so instead of talking to S3, you store data in cassandra via Pithos, and your client for S3 remains unchanged.