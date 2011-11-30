var fs = require('fs');
var url = require('url');
var express = require('express')
  , form = require('connect-form');
var mysql = require('mysql');
var client = mysql.createClient({
  user: 'root',
  password: ''
});

var app = express.createServer(
  form({ keepExtensions: true })
);
app.get('/', function(request, response){
        console.log('GET');
        retrieveData(request, response);
	});
	
app.post('/', function(request, response, next){
	response.end();
	postData(request,next);
	});

var postData = function (request,next){
	var file = null;
	request.form.complete(function(err, fields, files){
		if (err) {
			next(err);
		} else {
			updateDatabase(fields);
			file = files.file;
		}
		console.log(fields);
		console.log(file);
		moveFile(file);
	});
}

var moveFile = function (file){
	fs.rename(file.path, "/home/ec2-user/CSE520S/nodejs/images/"+file.name,function(err){
		console.log(err);
	});
}

var updateDatabase = function (elements){
	client.query('USE cse520S', function(error, results) {
		if(error) {
			console.log('ClientConnectionReady Error: ' + error.message);
			client.end();
			return;
		}		
		client.query("insert into sensor Set light = ?, temperature = ?,condition = ?,humidity = ?,pressure = ?, lat = ?, lon = ?" , [elements.light, elements.temperature,elements.condition,elements.humidity, elements.pressure,elements.lat, elements.lon]);		
	});    
}

var retrieveData = function (request, response){
    var items = new Array();
    client.query('USE cse520S', function(error, results) {
        if(error) {
            console.log('ClientConnectionReady Error: ' + error.message);
            client.end();
            return;
        }
        client.query("SELECT * FROM sensor ", function selectSensorData(error, results, fields) {
            if (error) {
                console.log('GetData Error: ' + error.message);
                client.end();
                return;
            }
            for (var i = 0; i < results.length; i++){
                items.push({latitude:results[i]['lat'],
                            longitude:results[i]['lon'],
                            light:results[i]['light'],
                            humidity:results[i]['humidity'],
                            pressure:results[i]['pressure'],
                            condition:results[i]['condition'],
                            temperature:results[i]['temperature'],
                            time:results[i]['time']
                            });
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
app.listen(1337);
console.log('Express app started on http://127.0.0.1:1337/');
