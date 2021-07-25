# WhatsApp Client Library for .NET Developer

Adalah library khusus untuk .NET Developer. Library ini dibangun di atas engine [whatsapp-web.js](https://github.com/pedroslopez/whatsapp-web.js/) untuk mempermudah komunikasi dengan aplikasi WhatsApp Web.

## Info Rilis dan Petunjuk Instalasi

Bisa Anda cek di http://wa-net.coding4ever.net/

## Persyaratan Sistem

* Windows 7, 8, 10 atau versi terbaru
* .NET Framework 4.5 atau versi terbaru
*  Node.js versi 13.14.0 (khusus Windows 7)
*  Node.js versi 14.16.x atau versi terbaru (untuk windows 8, 10 atau windows terbaru)

## Fitur

- Full mode siluman (headless/no window)
- Grab contacts untuk membaca kontak WA sehingga hasilnya bisa Anda simpan ke database
- Grab groups dan member untuk membaca data group berserta anggotanya sehingga hasilnya bisa Anda simpan ke database
- Bisa mengirim pesan ke nomor yang sudah terdaftar di kontak atau belum
- Mengirim pesan dengan gambar, audio, video, dan semua jenis dokumen
- Mengirim banyak pesan (broadcast)
- Bisa menyimpan gambar, audio, video, dan semua jenis dokumen dari pesan yang masuk (bisa ditentukan lokasi penyimpanannya)
- Archive/Delete Chat
- Listen/subscribe pesan yang masuk, jadi enggak perlu nambah objek timer lagi untuk membaca pesan masuk
- Listen/subscribe pesan yang dikirim, dengan fitur ini kita bisa mengecek apakah pesan sudah terkirim atau belum.
- Otomatis login (cukup sekali aja, scan qrcode wanya)
- Bisa dengan mudah diintegrasikan dengan semua jenis database

## Instalasi Cepat

* Install Node.js versi 13.14.0 (windows 7) atau 14.x.x (untuk windows 8, 10 atau windows terbaru)
* Clone repository [WhatsAppNETAPINodeJs](https://github.com/WhatsAppNETClient/WhatsAppNETAPINodeJs)
  - Jalankan command prompt, kemudian masuk ke folder WhatsAppNETAPINodeJs dengan menggunakan perintah CD.
  - Setelah itu ketik perintah: `npm install`, tunggu beberapa saat sampai proses instalasi selesai.
* Clone repository [Demo WhatsAppNETClient](https://github.com/WhatsAppNETClient/WhatsAppNETClient2)
  - Buka source code `Demo WhatsAppNETClient`
  - Klik kanan `Solution` -> `Rebuild Solution`
  - Jalankan project (F5), kemudian set lokasi folder `WhatsAppNETAPINodeJs`.