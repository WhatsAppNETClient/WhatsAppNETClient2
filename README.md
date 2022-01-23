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
* Otomatis menyimpan sesi login (jadi scan qr code WAnya cukup sekali saja)
* Mendukung penggunaan [multi account WA](https://github.com/WhatsAppNETClient/WhatsAppNETClientMultiAccount), untuk contohnya bisa Anda lihat di [https://github.com/WhatsAppNETClient/WhatsAppNETClientMultiAccount](https://github.com/WhatsAppNETClient/WhatsAppNETClientMultiAccount)
* Grab contacts untuk membaca kontak WA sehingga hasilnya bisa disimpan ke database
* Grab groups dan members untuk membaca data group beserta anggotanya sehingga hasilnya juga bisa disimpan ke database
* Mengirim pesan personal atau group
* Mengirim banyak pesan (broadcast)
* Mengirim pesan dengan gambar, audio, video, dan semua jenis dokumen
* Mengirim pesan dengan gambar, audio, video, dan semua jenis dokumen via URL
* Mengirim pesan dengan tipe `location`
* Mengirim pesan dengan tipe `button`
* Mengirim pesan dengan tipe `list` (tidak support untuk WA bisnis)
* ReplyMessage (quoted message)
* Bisa menyimpan gambar, audio, video, semua jenis dokumen termasuk vcard dari pesan yang masuk (bisa ditentukan sendiri lokasi penyimpanannya)
* Bisa juga membaca pesan dengan tipe `vcard` dan `location`
* Bisa membaca pesan dari group dan mendapatkan informasi pengirimnya
* Bisa mendapatkan nomor WA yang digunakan untuk scan QRCode
* Cek histori pesan berdasarkan nomor WA, jumlah pesan yang ditampilkan bisa diatur lewat parameter `limit`
* Subscribe event `ChangeState` untuk memonitoring perubahan status koneksi
* Subscribe event `GroupJoin` untuk memonitoring user yang join ke group
* Subscribe event `GroupLeave` untuk memonitoring user keluar dari group
* Subscribe event `ChangeBattery` untuk memonitoring status battery. Status battery akan diupdate secara berkala, dan jika ingin mendapatkan status battery terkini tinggal panggil method `GetBatteryStatus`
* Subscribe event `ReceiveMessage`, untuk memonitoring pesan masuk
* Subscribe event `ReceiveMessageStatus` untuk memonitoring status pesan yang dikirim berhasil atau gagal.
* Subscribe event `MessageAck` untuk memonitoring status pesan setelah di kirim (pending, sudah diterima/baca, dll)
* Tersedia fitur `REST API`, sehingga semua fitur library WhatsApp NET Client juga bisa diakses via `REST API` baik secara local maupun remote. Untuk contoh penggunaannya bisa Anda lihat di [https://github.com/WhatsAppNETClient/WhatsAppNETAPIRestApi](https://github.com/WhatsAppNETClient/WhatsAppNETAPIRestApi)
* Archive chat (semua atau berdasarkan nomor WA)
* Delete chat (semua atau berdasarkan nomor WA)
* Pengesetan status
* Logout
* Bisa dengan mudah diintegrasikan dengan semua jenis database

## Melaporkan Bug atau Error

Secara teknis dalam pengembangan sebuah aplikasi jelas tidak mungkin 100% bebas dari bug. Nah jika Anda menemukan bug atau error pada saat menggunakan library WhatsApp NET Client ini, silahkan Anda laporkan di halaman https://github.com/WhatsAppNETClient/WhatsAppNETClient2/issues