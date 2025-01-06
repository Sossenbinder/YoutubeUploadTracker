import { Input } from "@mantine/core";
import { IconSearch } from "@tabler/icons-react";

export default function Search() {
  return (
    <div className="flex flex-row gap-2">
      <Input
        placeholder="Search..."
        rightSection={<IconSearch className={"border rounded p-1"} size={24} />}
      />
    </div>
  );
}
