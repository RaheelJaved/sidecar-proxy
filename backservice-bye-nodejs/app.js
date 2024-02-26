const express = require('express');
//const logger = require('pino')();

const failureRate = parseInt(process.env.FAILURERATE || 0, 10);

const app = express();

app.get('/bye', async (req, res) => {
  logRequestHeaders(req, null);

  const randomValue = Math.floor(Math.random() * 100); // Generate random value between 0 and 99
  console.log(`*********** failureRate is ${failureRate}`);

  if (randomValue < failureRate) {

    console.log(`*********** randomValue is ${randomValue}, returning error`);
    res.status(500).send(`Random failure value: ${randomValue}`);
    return;
  }

  console.log(`*********** randomValue is ${randomValue}, returning success`);
  const message = `Bye from backservice at ${new Date().toLocaleTimeString()} - random value: ${randomValue}`;
  res.send(message);
});

function logRequestHeaders(req, logger) {
  var headersString = "HTTP Request Headers:\r\n";

  for (const [key, value] of Object.entries(req.headers)) {
    if(key.startsWith("x-fwd")) continue;
    headersString += `${key} = ${value}\r\n`;
  }
 
  console.log(headersString);  
}


const port = process.env.PORT || 6002;
app.listen(port, () => {
  console.log(`Server listening on port ${port}`);
});
