using StackExchange.Redis;


ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("localhost:1907", opt =>
{
});


ISubscriber subscriber = connection.GetSubscriber();


await subscriber.SubscribeAsync("mychannel.*", (channel, message) =>
 {
     Console.WriteLine($"Channel : {channel} , Message : {message}");
 });

Console.Read();


