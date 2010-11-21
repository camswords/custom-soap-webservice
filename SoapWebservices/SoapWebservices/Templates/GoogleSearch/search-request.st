<soapenv:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
			      xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
			      xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" 
			      xmlns:urn="urn:RandomGoogleSearch">
	<soapenv:Header/>
	<soapenv:Body>
		<urn:getRandomGoogleSearch soapenv:encodingStyle="http://schemas.xmlsoap.org/soap/encoding/">
			<word xsi:type="xsd:string">example2</word>
		</urn:getRandomGoogleSearch>
	</soapenv:Body>
</soapenv:Envelope>