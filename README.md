# Digital signature example with different programming languages

### <i>:stop_sign: All keys should be on PKCS8</i>

<hr>

## Object serialization contract

<ul>
    <li>Single line json string format  (Json output should not be indented)</li>
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

## Java example

```java
public class Main {
    public static void main(String[] args) {
        final String privateKey = "Your private key";

        final String publicKey = "Your public key";


        try {

            Payment payment = new Payment();

            payment.setAmount(48.1500000);
            payment.setDescription("Transfer");

            System.out.println(payment.toJsonString());

            // Sign payment 
            String signature = RsaSignatureManager.sign(privateKey, payment.toJsonString());

            // Verify payment signature
            if (RsaSignatureManager.verify(publicKey, payment.toJsonString(), signature)) {
                System.out.println("Signature has been verified");
            }
        } catch (NoSuchAlgorithmException e) {
            throw new RuntimeException(e);
        } catch (InvalidKeySpecException e) {
            throw new RuntimeException(e);
        } catch (InvalidKeyException e) {
            throw new RuntimeException(e);
        } catch (SignatureException e) {
            throw new RuntimeException(e);
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }

    }
}
```