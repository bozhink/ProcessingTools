#
# CreateSelfSignedCertificate.ps1
#

# See https://medium.com/the-new-control-plane/generating-self-signed-certificates-on-windows-7812a600c2d8

$now = Get-Date
$extended_date = $now.AddYears(3)

$cert = New-SelfSignedCertificate -CertStoreLocation Cert:\LocalMachine\My -DnsName example.com # -NotAfter $extended_date

# Using `mmc`, we can see the certificate in the local computer store. Although it shows `Client Authentication`, it is valid for `Server Authentication` as well

$pwd = ConvertTo-SecureString 'password1234' -Force -AsPlainText

$path = 'Cert:\LocalMachine\My\' + $cert.Thumbprint

Export-PfxCertificate -Cert $path -FilePath $env:TEMP\powershellcert.pfx -Password $pwd
