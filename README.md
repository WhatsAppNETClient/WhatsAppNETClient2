# WhatsApp Client Library for .NET Developer

Adalah library khusus untuk .NET Developer. Library ini dibangun di atas engine [whatsapp-web.js](https://github.com/pedroslopez/whatsapp-web.js/) untuk mempermudah komunikasi dengan aplikasi WhatsApp Web.

## Info Rilis dan Petunjuk Instalasi

Bisa Anda cek di http://wa-net.coding4ever.net/

## Persyaratan Sistem

* Windows 8, 10 dan windows versi terbaru
* .NET Framework 4.5 dan .NET versi terbaru
* Node.js versi 14.16.x atau versi terbaru
* [Software git](https://git-scm.com/downloads) (version control)

## Fitur

* Full mode *siluman* (headless/no window). Anda bisa mengatakan selamat tinggal kepada  chrome/firefox browser yang biasanya muncul untuk menjalankan WhatsApp Web.
* Grab contacts untuk membaca kontak WA sehingga hasilnya bisa disimpan ke database
* Grab groups dan members untuk membaca data group berserta anggotanya sehingga hasilnya juga bisa disimpan ke database
* Mengirim pesan
* Mengirim banyak pesan (broadcast)
* Mengirim pesan dengan gambar, audio, video, dan semua jenis dokumen
* Bisa menyimpan gambar, audio, video, dan semua jenis dokumen dari pesan yang masuk (bisa ditentukan sendiri lokasi penyimpanannya)
* Bisa juga membaca pesan dengan tipe `contact` dan `location`
* Archive chat
* Delete chat
* Listen/subscribe pesan yang masuk, jadi enggak perlu nambah objek timer lagi untuk membaca pesan masuk
* Listen/subscribe pesan yang dikirim, dengan fitur ini kita bisa mengecek apakah pesan yang dikirim berhasil atau gagal
* Otomatis menyimpan sesi login (jadi scan qr code WAnya cukup sekali saja)
* Bisa dengan mudah diintegrasikan dengan semua jenis database

## Melaporkan Bug atau Error

Secara teknis dalam pengembangan sebuah aplikasi jelas tidak mungkin 100% bebas dari bug. Nah jika Anda menemukan bug atau error pada saat menggunakan library WhatsApp NET Client ini, silahkan Anda laporkan di halaman https://github.com/WhatsAppNETClient/WhatsAppNETClient2/issues