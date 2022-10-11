using DSA.Serialization;
using DSA.SigningAlgorithms;

const string privateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIIEowIBAAKCAQEAxRf9bx3wjpvX2MPALHkRAQ6tUkcizlZCZsib6a4D/r4LhgmN
iR1Ybekv6avE0pz3xKbiLEGQ1//qbdebmnBbJ9aJFQlQHSSz9fEbFXPOobhu4Ci7
iOo2ktFoDU6ZFn64z2ESjYEPUzCYaCUOUrljScCmfqkRKqYmpfpJl168zmtT3dEm
pdOx7hxwhy4qa/pneq01TOexq04FZYjqcMun7ZLgaTxpqp7z4R1yavFSLeCBdIXu
MfJAgbG7+8m5lkeah70sIrFzTv61khwjkXl5I0lBw25LJ6abd89tutf7bNXv/5Cp
wn16SoDbHgZBPOgPf8+XOXTMmFwcRV7Q9Dw0bQIDAQABAoIBAA0Wir3WPyM5Nhwu
45QQdhCb6OaN0sHg+Ifayn+6DoncFOfRupRgXn7t/UCHJ4rU4Z8tddd6gyPU2rxw
lOSskWET3gaHl8pXpZuvZx0Fk7MpjDNWgR2WcAlP3pkDnybfQ2aCC7lZ2Kqtj3Fb
BJh5tx4urN+8Wmd6oRZkiH6EzATo/ZUfGxRyoag5E85xdvFY+G4tVXrR/3xevbJm
ectZjXHvLIJu+AnhoB6I/TK/66M9fhV091Y2zOowOA0zcXhDBCsg1/79BdMuyq87
At8MX+W8PqhPlmIRihW9E6TkqpvWIFe8WxL7tDTuaVafd+dolT7oL2korSW8KMEQ
wiv9vAECgYEA/ztQ4tgbLO7FUi9FXrec5IX6lgdM3MdZ5RsnPcsPgzqf7W2fU/cQ
FPZWZOXM6V+U7ENiMIoaTl7YDetBfbVBrIHtj0XWgc655Cg0Bgxqk7lQ4npz3hTT
O/1OyBQXW/E7c0z5M+1M6myO22x9a5pAiRcQDJI1b9peUWVb7Cfmv8sCgYEAxa/f
T9RxknoAw4xOID65K/J577vA0EfSl8C5bXsvZAX4sBCV4WqE3uhcAnoEgRcrgUD1
z9JOWZt9UBlEjCGzUPSTMZQW5UDwPNL7waJHbcKq7/VRF1Q988GcVJglW7BEH6Wc
ADl/Mqcz+6H1KA3rfI6L4Dindj9Th0YKO5PeZacCgYAN5D2XyylXfbtxqK7+48F/
Uqd76PPPBG6pIJw3KckzXEp+ErxcRpMygOHc5gEMJq6lWVmLyMh7wX/C6i4LWIU1
jXDPnn55XDYT6/hA6AYY+jojyYRq7vk8R6I1E5rd3zhcIzFhGSZZkAUKflIpPCEN
FRtDg+Wyq9D8XybsqJNPlwKBgQCsd3uD0fwjpnz0HXwrdY1nYewv0sWs8HwyGNmt
QkNeKDeGo3Gfb6bsMhvTrVAwr14tLddGsFvaoWT6dJDxEJW4kye3oVM4PydYaVlq
3YEjqSoonwVYLQ6/krIou3QcQfOtCHbJW2A1eNpJNksD3uELkfFGdSbkauscoWZc
A+2LEQKBgBE3YSSB9mxOSyNpszpaLWDO6B2Q53OsTqd2HYA+59Vwu5XGraumJad7
uskwTUHgLeI+3I5ZuXsyyVNBMa9ruCQH8WxK7mC86PBpbhDBaZu41jMcwhMbNSyt
qLVbMDeFJbNI/x0cvwkKivTTI9iJlYvse4hqSCrY5JXo0CdWObsS
-----END RSA PRIVATE KEY-----
";

const string publicKey = @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxRf9bx3wjpvX2MPALHkR
AQ6tUkcizlZCZsib6a4D/r4LhgmNiR1Ybekv6avE0pz3xKbiLEGQ1//qbdebmnBb
J9aJFQlQHSSz9fEbFXPOobhu4Ci7iOo2ktFoDU6ZFn64z2ESjYEPUzCYaCUOUrlj
ScCmfqkRKqYmpfpJl168zmtT3dEmpdOx7hxwhy4qa/pneq01TOexq04FZYjqcMun
7ZLgaTxpqp7z4R1yavFSLeCBdIXuMfJAgbG7+8m5lkeah70sIrFzTv61khwjkXl5
I0lBw25LJ6abd89tutf7bNXv/5Cpwn16SoDbHgZBPOgPf8+XOXTMmFwcRV7Q9Dw0
bQIDAQAB
-----END PUBLIC KEY-----";

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