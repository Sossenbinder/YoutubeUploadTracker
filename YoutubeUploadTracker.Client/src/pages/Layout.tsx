import React from "react";
import { AppShell } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import Navbar from "@root/src/features/common/components/navbar/NavBar";
import Sidebar from "../features/common/components/sidebar/SideBar";

export default function Layout({ children }: React.PropsWithChildren) {
  const [opened, { toggle }] = useDisclosure(false);

  return (
    <AppShell
      header={{ height: 60 }}
      navbar={{ width: 300, breakpoint: "sm", collapsed: { mobile: !opened } }}
      styles={{
        root: {
          height: "100%",
          width: "100%",
        },
        main: {
          height: "100%",
          width: "100%",
        },
      }}
      padding="md">
      <AppShell.Header>
        <Navbar opened={opened} toggle={toggle} />
      </AppShell.Header>
      <AppShell.Navbar p="md">
        <Sidebar />
      </AppShell.Navbar>
      <AppShell.Main>{children}</AppShell.Main>
    </AppShell>
  );
}
