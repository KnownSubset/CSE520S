var http = require('http');
http.createServer(function (req, response) {
  console.log("HI");
  var tweets = {items:[
	    {latitude:1, longitude:2,type:"height"},
	    {latitude:1.25, longitude:2.25,type:"height"},
	    {latitude:1.5, longitude:2.5,type:"height"},
	    {latitude:1.75, longitude:2.75,type:"height"}
    ]};  
  response.writeHead(200, {"Content-Type": "text/plain"});
  response.write(tweets);
  response.end();
}).listen(1337, "127.0.0.1");
console.log('Server running at http://127.0.0.1:1337/');
