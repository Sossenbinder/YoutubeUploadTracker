import "@mantine/core/styles.css";
import { MantineProvider } from "@mantine/core";

export default function Root({ children }: React.PropsWithChildren) {
  return <MantineProvider>{children}</MantineProvider>;
}
