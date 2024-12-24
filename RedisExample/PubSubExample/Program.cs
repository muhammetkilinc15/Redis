using StackExchange.Redis;

ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("localhost:1907", opt =>
{

});


ISubscriber subscriber = connection.GetSubscriber();


while (true)
{
    Console.WriteLine("Enter message : ");
    string message = Console.ReadLine();
    await subscriber.PublishAsync(channel:"mychannel.*", message);
}
