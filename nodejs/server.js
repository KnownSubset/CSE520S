var http = require('http');
var url = require('url');
var sys = require('sys');
var mysql = require('mysql');
var fs = require('fs');
var client = mysql.createClient({
  user: 'root',
  password: 'mysql'
});

http.createServer(function (request, response) {
    if (request.method == 'GET') {
        console.log('GET');
        retrieveData(request, response);
    } else if (request.method == 'POST'){
        console.log('POST');
        postData(request, response);
	response.end();
    }
}).listen(1337, "127.0.0.1");

var writeFile = function (){
	var outstream = fs.createWriteStream('filename');
}

var postData = function (request){

	    var body = [];
	    request.on("data", function (data) {
		body.push(data);
	    });
	    request.on('end', function () {
	 	var elements = {};
		console.log(body);
		var json = body.join('');
		console.log(json);
		elements = JSON.parse(json);
		client.query('USE cse520S', function(error, results) {
			if(error) {
			    console.log('ClientConnectionReady Error: ' + error.message);
			    client.end();
			    return;
			}
			client.query("insert into sensor Set type = ?, value = ?, lat = ?, lon = ?" , [elements.type, elements.value, elements.lat, elements.lon]);

		    });
	    });
	
    
}

var retrieveData = function (request, response){
    var items = new Array();
    var params = url.parse(request.url).query;
    var type = params.split('=')[1];
    client.query('USE cse520S', function(error, results) {
        if(error) {
            console.log('ClientConnectionReady Error: ' + error.message);
            client.end();
            return;
        }
        client.query("SELECT * FROM sensor where type='"+type+"'", function selectSensorData(error, results, fields) {
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
            var sensorData = {items: items};
            var body = JSON.stringify(sensorData);
            response.writeHead(200, {
                'Content-Type': 'text/json',
                'Access-Control-Allow-Origin': '*',
                'Content-Length': body.length});
            response.write(body);
            response.end();
        });

    });
}
console.log('Server running at http://127.0.0.1:1337/');
