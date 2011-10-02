var http = require('http');
var url = require("url");

http.createServer(function (req, response) {
  
  var params = url.parse(req.url).query;
  var type = params.split('=')[1];
  var tweets = {items:[
	    {latitude:-34.1, longitude:152,type:type},
	    {latitude:-34.25, longitude:152.25,type:type},
	    {latitude:-34.5, longitude:152.5,type:type},
	    {latitude:-34.75, longitude:152.75,type:type}
    ]};  
  var body = JSON.stringify(tweets);
  response.writeHead(200, {	    
	  'Content-Type': 'text/json',
	  'Access-Control-Allow-Origin': '*',
	 'Content-Length': body.length});
  response.write(body);
  response.end();
}).listen(1337, "127.0.0.1");
console.log('Server running at http://127.0.0.1:1337/');
