var http = require('http');
var url = require('url');
var sys = require('sys');
var Client = require('mysql').Client;
var client = new Client();
 
client.user = 'root';
client.password = '';

http.createServer(function (req, response) {
  var items = new Array();
  var params = url.parse(req.url).query;
  var type = params.split('=')[1];
  client.query('USE cse520S', function(error, results) {
        if(error) {
            console.log('ClientConnectionReady Error: ' + error.message);
            client.end();
            return;
        }
        client.query("SELECT * FROM sensor where type='"+type+"'", function selectCb(error, results, fields) {
	      if (error) {
		  console.log('GetData Error: ' + error.message);
		  client.end();
		  return;
	      }
	      for (var i = 0; i < results.length; i++){
	      	items.push({latitude:results[i]['lat'],
			    longitude:results[i]['lon'], 
			    type:results[i]['type'], 
			    value:results[i]['value']});
              }
	      var tweets = {items: items};  
	      var body = JSON.stringify(tweets);
	      response.writeHead(200, {	    
		  'Content-Type': 'text/json',
		  'Access-Control-Allow-Origin': '*',
		  'Content-Length': body.length});
	      response.write(body);
	      response.end();
	});

    });
}).listen(1337, "127.0.0.1");
console.log('Server running at http://127.0.0.1:1337/');
