# Digital signature example with different programming languages

## Object serialization contract

<ul>
    <li>Single line format json string (Json output should not be indented)</li>
    <li>Removing the null properties inside the json string</li>
    <li>Alphabetical order of all the object properties</li>
    <li>Converting numbers to string</li>
    <li> Formatting float number without trailing zeros
    Fractional digits of a float number should be limited to 5   digits without trailing zeros (the value can be customized)</li>
</ul>

## C# example

```csharp
const string privateKey = "Your private key";
const string publicKey = "Your public key";

var payment = new
{
    Amount = 10.2500000m,
    Description = "Transfer"
};

Console.WriteLine(payment.ToJsonString());

// Sign payment 
var signature = RsaSignatureManager.Sign(payment, privateKey);

// Verify payment signature
Console.WriteLine(RsaSignatureManager.Verify(payment, signature, publicKey)
    ? "Signature has been verified"
    : "Invalid signature");
```