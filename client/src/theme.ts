import { createTheme } from '@material-ui/core/styles';
import { lightGreen, amber } from '@material-ui/core/colors';

const theme = createTheme({
    palette: {
        primary: lightGreen,
        secondary: amber,
    },
});

export default theme;
