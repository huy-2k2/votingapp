// src/vuetify.js
import 'vuetify/styles'; // Import các style cơ bản của Vuetify
import { createVuetify } from 'vuetify';
import { aliases, mdi } from 'vuetify/iconsets/mdi-svg'; // Đảm bảo có icon set nếu cần

// Tạo instance Vuetify với cấu hình cơ bản
const vuetify = createVuetify({
  icons: {
    defaultSet: 'mdi', // Sử dụng icon set Material Design Icons
    aliases,
    sets: {
      mdi,
    },
  },
  theme: {
    defaultTheme: 'light',
  },
});

export default vuetify;
