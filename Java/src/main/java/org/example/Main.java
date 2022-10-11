package org.example;

import com.fasterxml.jackson.core.JsonProcessingException;
import org.example.dsa.RsaSignatureManager;

import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;
import java.security.SignatureException;
import java.security.spec.InvalidKeySpecException;

public class Main {
    public static void main(String[] args) {
        final String privateKey = "-----BEGIN PRIVATE KEY-----\n" +
                "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDFF/1vHfCOm9fY\n" +
                "w8AseREBDq1SRyLOVkJmyJvprgP+vguGCY2JHVht6S/pq8TSnPfEpuIsQZDX/+pt\n" +
                "15uacFsn1okVCVAdJLP18RsVc86huG7gKLuI6jaS0WgNTpkWfrjPYRKNgQ9TMJho\n" +
                "JQ5SuWNJwKZ+qREqpial+kmXXrzOa1Pd0Sal07HuHHCHLipr+md6rTVM57GrTgVl\n" +
                "iOpwy6ftkuBpPGmqnvPhHXJq8VIt4IF0he4x8kCBsbv7ybmWR5qHvSwisXNO/rWS\n" +
                "HCOReXkjSUHDbksnppt3z2261/ts1e//kKnCfXpKgNseBkE86A9/z5c5dMyYXBxF\n" +
                "XtD0PDRtAgMBAAECggEADRaKvdY/Izk2HC7jlBB2EJvo5o3SweD4h9rKf7oOidwU\n" +
                "59G6lGBefu39QIcnitThny1113qDI9TavHCU5KyRYRPeBoeXylelm69nHQWTsymM\n" +
                "M1aBHZZwCU/emQOfJt9DZoILuVnYqq2PcVsEmHm3Hi6s37xaZ3qhFmSIfoTMBOj9\n" +
                "lR8bFHKhqDkTznF28Vj4bi1VetH/fF69smZ5y1mNce8sgm74CeGgHoj9Mr/roz1+\n" +
                "FXT3VjbM6jA4DTNxeEMEKyDX/v0F0y7KrzsC3wxf5bw+qE+WYhGKFb0TpOSqm9Yg\n" +
                "V7xbEvu0NO5pVp9352iVPugvaSitJbwowRDCK/28AQKBgQD/O1Di2Bss7sVSL0Ve\n" +
                "t5zkhfqWB0zcx1nlGyc9yw+DOp/tbZ9T9xAU9lZk5czpX5TsQ2IwihpOXtgN60F9\n" +
                "tUGsge2PRdaBzrnkKDQGDGqTuVDienPeFNM7/U7IFBdb8TtzTPkz7UzqbI7bbH1r\n" +
                "mkCJFxAMkjVv2l5RZVvsJ+a/ywKBgQDFr99P1HGSegDDjE4gPrkr8nnvu8DQR9KX\n" +
                "wLltey9kBfiwEJXhaoTe6FwCegSBFyuBQPXP0k5Zm31QGUSMIbNQ9JMxlBblQPA8\n" +
                "0vvBokdtwqrv9VEXVD3zwZxUmCVbsEQfpZwAOX8ypzP7ofUoDet8jovgOKd2P1OH\n" +
                "Rgo7k95lpwKBgA3kPZfLKVd9u3Gorv7jwX9Sp3vo888EbqkgnDcpyTNcSn4SvFxG\n" +
                "kzKA4dzmAQwmrqVZWYvIyHvBf8LqLgtYhTWNcM+efnlcNhPr+EDoBhj6OiPJhGru\n" +
                "+TxHojUTmt3fOFwjMWEZJlmQBQp+Uik8IQ0VG0OD5bKr0PxfJuyok0+XAoGBAKx3\n" +
                "e4PR/COmfPQdfCt1jWdh7C/SxazwfDIY2a1CQ14oN4ajcZ9vpuwyG9OtUDCvXi0t\n" +
                "10awW9qhZPp0kPEQlbiTJ7ehUzg/J1hpWWrdgSOpKiifBVgtDr+Ssii7dBxB860I\n" +
                "dslbYDV42kk2SwPe4QuR8UZ1JuRq6xyhZlwD7YsRAoGAETdhJIH2bE5LI2mzOlot\n" +
                "YM7oHZDnc6xOp3YdgD7n1XC7lcatq6Ylp3u6yTBNQeAt4j7cjlm5ezLJU0Exr2u4\n" +
                "JAfxbEruYLzo8GluEMFpm7jWMxzCExs1LK2otVswN4Uls0j/HRy/CQqK9NMj2ImV\n" +
                "i+x7iGpIKtjklejQJ1Y5uxI=\n" +
                "-----END PRIVATE KEY-----";

        final String publicKey = "-----BEGIN PUBLIC KEY-----\n" +
                "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxRf9bx3wjpvX2MPALHkR\n" +
                "AQ6tUkcizlZCZsib6a4D/r4LhgmNiR1Ybekv6avE0pz3xKbiLEGQ1//qbdebmnBb\n" +
                "J9aJFQlQHSSz9fEbFXPOobhu4Ci7iOo2ktFoDU6ZFn64z2ESjYEPUzCYaCUOUrlj\n" +
                "ScCmfqkRKqYmpfpJl168zmtT3dEmpdOx7hxwhy4qa/pneq01TOexq04FZYjqcMun\n" +
                "7ZLgaTxpqp7z4R1yavFSLeCBdIXuMfJAgbG7+8m5lkeah70sIrFzTv61khwjkXl5\n" +
                "I0lBw25LJ6abd89tutf7bNXv/5Cpwn16SoDbHgZBPOgPf8+XOXTMmFwcRV7Q9Dw0\n" +
                "bQIDAQAB\n";


        try {
            Payment payment = new Payment();

            payment.setAmount(48.1500000);
            payment.setDescription("Transfer");

            System.out.println(payment.toJsonString());

            String signature = RsaSignatureManager.sign(privateKey, payment.toJsonString());
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
