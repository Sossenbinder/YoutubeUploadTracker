import "@mantine/core/styles.css";
import "./index.css";
import { MantineProvider, createTheme } from "@mantine/core";

const theme = createTheme({
  cursorType: "pointer",
  fontSizes: {
    xxl: "2rem",
  },
});

export default function Root({ children }: React.PropsWithChildren) {
  return <MantineProvider theme={theme}>{children}</MantineProvider>;
}
